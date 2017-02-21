using Prism.Regions;
using System;
using System.ComponentModel.Composition;

namespace XIAOWEN.INFRASTRUCTURE.Behaviors
{
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        public static string key = "AutoPopulateExportedViewsBehavior";
        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }
        private void AddRegisteredViews()
        {
            if (Region != null)
            {
                foreach (var viewEntry in RegisteredViews)
                {
                    if (viewEntry.Metadata.RegionName == Region.Name)
                    {
                        var view = viewEntry.Value;

                        if (!Region.Views.Contains(view))
                        {
                            Region.Add(view);
                        }
                    }
                }
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}
