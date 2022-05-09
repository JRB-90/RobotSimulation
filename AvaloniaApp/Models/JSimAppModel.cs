﻿using Avalonia.Controls;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using JSim.Avalonia.Shared;
using JSim.Avalonia.ViewModels;
using JSim.BasicBootstrapper;
using JSim.Core;
using JSim.Core.Importer;
using JSim.Core.Maths;
using JSim.Core.Render;
using JSim.Core.Render.GeometryBuilders;
using JSim.Core.SceneGraph;
using JSim.Logging;
using JSim.OpenTK;
using System.Diagnostics;
using System.Threading;

namespace AvaloniaApp.Models
{
    public class JSimAppModel
    {
        readonly ISimApplication app;
        readonly IOpenTKControlFactory openTKControlFactory;

        public JSimAppModel(Window window)
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();

            container.Install(
                new BasicApplicationInstaller(),
                Log4NetInstaller.FromEmbedded("log4netconsole.config"),
                new BasicSceneManagerInstaller(),
                new OpenTKInstaller()
            );

            app = container.Resolve<ISimApplication>();
            openTKControlFactory = container.Resolve<IOpenTKControlFactory>();

            var scene = app.SceneManager.CurrentScene;

            var assembly1 = scene.Root.CreateNewAssembly("Assembly1");
            var entity1 = assembly1.CreateNewEntity("Entity1");
            var entity2 = assembly1.CreateNewEntity("Entity2");

            var cube = CubeBuilder.Build(0.2, 0.2, 0.2);
            var cubeSolid = entity2.GeometryContainer.Root.CreateChildGeometry("Cube");
            entity2.LocalFrame = new Transform3D(0.5, 0.5, 0.1, 0.0, 0.0, 15.0);
            cubeSolid.IsVisible = false;
            cubeSolid.SetDrawingData(cube.Item1, cube.Item2);
            cubeSolid.GeometryType = GeometryType.Solid;
            cubeSolid.Material.Color = new Color(1.0f, 0.0f, 0.0f);
            cubeSolid.Material.Shading = ShadingType.Flat;

            double size = 1.0;
            var lineVerts =
                new Vertex[]
                {
                    new Vertex(0, new Vector3D(0, 0, 0)),
                    new Vertex(1, new Vector3D(size, 0, 0)),
                    new Vertex(2, new Vector3D(0, 0, 0)),
                    new Vertex(3, new Vector3D(0, size, 0)),
                    new Vertex(4, new Vector3D(0, 0, 0)),
                    new Vertex(5, new Vector3D(0, 0, size))
                };

            var XIndices = new uint[] { 0, 1 };
            var YIndices = new uint[] { 2, 3 };
            var ZIndices = new uint[] { 4, 5 };

            var xAxis = entity1.GeometryContainer.Root.CreateChildGeometry("XAxis");
            xAxis.SetDrawingData(lineVerts, XIndices);
            xAxis.GeometryType = GeometryType.Wireframe;
            xAxis.Material.Color = new Color(1.0f, 0.0f, 0.0f);
            xAxis.IsVisible = false;

            var yAxis = entity1.GeometryContainer.Root.CreateChildGeometry("YAxis");
            yAxis.SetDrawingData(lineVerts, YIndices);
            yAxis.GeometryType = GeometryType.Wireframe;
            yAxis.Material.Color = new Color(0.0f, 1.0f, 0.0f);
            yAxis.IsVisible = false;

            var zAxis = entity1.GeometryContainer.Root.CreateChildGeometry("ZAxis");
            zAxis.SetDrawingData(lineVerts, ZIndices);
            zAxis.GeometryType = GeometryType.Wireframe;
            zAxis.Material.Color = new Color(0.0f, 0.0f, 1.0f);
            zAxis.IsVisible = false;

            //var entity3 = 
            //    app.SceneManager.ModelImporter.LoadModel(
            //        @"C:\Development\Test\Suzanne.stl", 
            //        assembly1
            //    );

            var entity3 =
                app.SceneManager.ModelImporter.LoadModel(
                    @"C:\Development\Test\fanuc.3ds",
                    assembly1
                );

            var inputManager = new InputManager(window);
            var dialogManager = new DialogManager(window);

            MainMenuVM =
                new MainMenuViewModel(
                    app,
                    dialogManager
                );

            SceneTreeVM =
                new SceneTreeViewModel(
                    app.SceneManager,
                    inputManager,
                    dialogManager
                );

            SceneObjectVM =
                new SceneObjectViewModel(
                    app.SceneManager
                );

            app.SceneManager.CurrentScene.SelectionManager.SetSingleSelection(entity2);
            app.SceneManager.CurrentSceneChanged += OnCurrentSceneChanged;

            entity = entity1;
            timer = new Timer(new TimerCallback(Update), null, 100, 10);
        }

        public MainMenuViewModel MainMenuVM { get; }

        public SceneTreeViewModel SceneTreeVM { get; }

        public SceneObjectViewModel SceneObjectVM { get; }

        public Control Create3DControl()
        {
            OpenTKControl control = openTKControlFactory.CreateControl();
            control.Scene = app.SceneManager.CurrentScene;
            
            if (control.Camera != null)
            {
                control.Camera.PositionInWorld = new Transform3D(3, 3, 3, 0, 0, 0);
                control.Camera.LookAtPoint(Vector3D.Origin, -Vector3D.UnitZ);
            }
            
            app.SurfaceManager.AddSurface(control);

            return control;
        }

        private void OnCurrentSceneChanged(object sender, CurrentSceneChangedEventArgs e)
        {
            foreach (var surface in app.SurfaceManager.Surfaces)
            {
                surface.Scene = app.SceneManager.CurrentScene;
            }
        }

        private void Update(object? state)
        {
            var rx = _stopwatch.Elapsed.TotalSeconds * 20;
            var ry = _stopwatch.Elapsed.TotalSeconds * 30;
            var rz = _stopwatch.Elapsed.TotalSeconds * 40;
            //entity.LocalFrame = new Transform3D(0.0, 0.0, 0.0, rx, ry, rz);
            
            // TODO - Make render call happen on entity modified

            foreach (var surface in app.SurfaceManager.Surfaces)
            {
                surface.RequestRender();
            }
        }

        private ISceneEntity entity;
        private Timer timer;
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    }
}
