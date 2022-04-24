﻿using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JSim.Avalonia.Controls;
using JSim.Core.Display;
using JSim.Core.Render;

namespace JSim.OpenTK
{
    /// <summary>
    /// Installs an opentk based rendering backend.
    /// </summary>
    public class OpenTKInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IGeometryContainer>()
                .Named("GeometryContainer")
                .ImplementedBy<GeometryContainer>()
                .LifestyleTransient()
             );

            container.Register(
                Component.For<IGeometry>()
                .Named("OpenTKGeometry")
                .ImplementedBy<OpenTKGeometry>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IGeometryCreator>()
                .Named("GeometryCreator")
                .ImplementedBy<GeometryCreator>()
                .LifestyleTransient()
            );
            container.Register(
                Component.For<IGeometryCreatorFactory>()
                .AsFactory()
            );

            container.Register(
                Component.For<IRenderingEngine>()
                .Named("OpenTKRenderingEngine")
                .ImplementedBy<OpenTKRenderingEngine>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<IRenderingManager>()
                .Named("RenderingManager")
                .ImplementedBy<RenderingManager>()
                .LifestyleSingleton()
            );

            container.Register(
                Component.For<IRenderingSurface>()
                .Named("DummyRenderingSurface")
                .ImplementedBy<OpenTKControl>()
                .LifestyleTransient()
            );

            container.Register(
                Component.For<IDisplayManager>()
                .Named("DisplayManager")
                .ImplementedBy<DisplayManager>()
                .LifestyleSingleton()
            );
        }
    }
}
