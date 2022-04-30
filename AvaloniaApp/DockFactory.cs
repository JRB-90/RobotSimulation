using Avalonia.Data;
using AvaloniaApp.Models;
using AvaloniaApp.ViewModels;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApp
{
    public class DockFactory : Factory
    {
        public DockFactory(JSimAppModel context)
        {
            this.context = context;
        }

        public override IRootDock CreateLayout()
        {
            var documentView3D = 
                new View3DViewModel(
                    "3DView",
                    "3D View",
                    context.Create3DControl()
                );

            var toolSceneTree =
                new ToolSceneTreeViewModel(
                    "SceneTreeView",
                    "Scene Tree",
                    context.SceneTreeVM
                );

            var toolSceneObjectData =
                new ToolSceneObjectDataViewModel(
                    "SceneObjectView",
                    "Scene Object Data",
                    context.SceneObjectVM
                );

            var documentDock = new DocumentDock
            {
                Id = "DocumentsPane",
                Title = "DocumentsPane",
                Proportion = double.NaN,
                ActiveDockable = documentView3D,
                VisibleDockables = CreateList<IDockable>(documentView3D)
            };

            documentDock.CanCreateDocument = true;
            documentDock.CreateDocument = ReactiveCommand.Create(() =>
            {
                var index = documentDock.VisibleDockables?.Count + 1;

                var document =
                    new View3DViewModel(
                        $"3DView{index}", 
                        $"3D View {index}", 
                        context.Create3DControl()
                    );
                
                AddDockable(documentDock, document);
                SetActiveDockable(document);
                SetFocusedDockable(documentDock, document);
            });

            var mainLayout = new ProportionalDock
            {
                Id = "MainLayout",
                Title = "MainLayout",
                Proportion = double.NaN,
                Orientation = Orientation.Horizontal,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new ProportionalDock
                    {
                        Id = "LeftPane",
                        Title = "LeftPane",
                        Proportion = double.NaN,
                        Orientation = Orientation.Vertical,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                Id = "LeftPaneTop",
                                Title = "LeftPaneTop",
                                Proportion = double.NaN,
                                ActiveDockable = toolSceneTree,
                                VisibleDockables = CreateList<IDockable>(toolSceneTree),
                                Alignment = Alignment.Left,
                                GripMode = GripMode.Visible
                            }
                        )
                    },
                    new ProportionalDockSplitter()
                    {
                        Id = "LeftSplitter",
                        Title = "LeftSplitter"
                    },
                    documentDock,
                    new ProportionalDockSplitter()
                    {
                        Id = "RightSplitter",
                        Title = "RightSplitter"
                    },
                    new ProportionalDock
                    {
                        Id = "RightPane",
                        Title = "RightPane",
                        Proportion = double.NaN,
                        Orientation = Orientation.Vertical,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                Id = "RightPaneTop",
                                Title = "RightPaneTop",
                                Proportion = double.NaN,
                                ActiveDockable = toolSceneObjectData,
                                VisibleDockables = CreateList<IDockable>(toolSceneObjectData),
                                Alignment = Alignment.Right,
                                GripMode = GripMode.Visible
                            }
                        )
                    }
                )
            };

            var mainView = new MainViewModel
            {
                Id = "Main",
                Title = "Main",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var root = CreateRootDock();

            root.Id = "Root";
            root.Title = "Root";
            root.ActiveDockable = mainView;
            root.DefaultDockable = mainView;
            root.VisibleDockables = CreateList<IDockable>(mainView);

            this.documentDock = documentDock;

            return root;
        }

        public override void InitLayout(IDockable layout)
        {
            ContextLocator = new Dictionary<string, Func<object>>
            {
                [nameof(IRootDock)] = () => context,
                [nameof(IProportionalDock)] = () => context,
                [nameof(IDocumentDock)] = () => context,
                [nameof(IToolDock)] = () => context,
                [nameof(IProportionalDockSplitter)] = () => context,
                [nameof(IDockWindow)] = () => context,
                [nameof(IDocument)] = () => context,
                [nameof(ITool)] = () => context,
                ["3DView"] = () => context.Create3DControl(),
                ["SceneTreeView"] = () => context.SceneTreeVM,
                ["SceneObjectData"] = () => context.SceneObjectVM,
                ["LeftPane"] = () => context,
                ["LeftPaneTop"] = () => context,
                ["LeftPaneTopSplitter"] = () => context,
                ["LeftPaneBottom"] = () => context,
                ["RightPane"] = () => context,
                ["RightPaneTop"] = () => context,
                ["RightPaneTopSplitter"] = () => context,
                ["RightPaneBottom"] = () => context,
                ["DocumentsPane"] = () => context,
                ["MainLayout"] = () => context,
                ["LeftSplitter"] = () => context,
                ["RightSplitter"] = () => context,
                ["MainLayout"] = () => context,
                ["Main"] = () => context,
            };

            HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () =>
                {
                    var hostWindow = new HostWindow()
                    {
                        [!HostWindow.TitleProperty] = new Binding("ActiveDockable.Title")
                    };
                    return hostWindow;
                }
            };

            DockableLocator = new Dictionary<string, Func<IDockable?>>();

            base.InitLayout(layout);

            if (documentDock != null)
            {
                SetActiveDockable(documentDock);
                SetFocusedDockable(
                    documentDock, 
                    documentDock.VisibleDockables?.FirstOrDefault()
                );
            }
        }

        private IDocumentDock? documentDock;
        private readonly JSimAppModel context;
    }
}
