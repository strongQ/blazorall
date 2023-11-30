using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Global.Nav.Model
{
    public class NavModel
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public string? Href { get; set; }

        public string Icon { get; set; }

        public string ParentIcon { get; set; }

        public string Title { get; set; }

        public string FullTitle { get; set; }

        public bool Hide { get; set; }

        public bool Active { get; set; }

        public string? Target { get; set; }

        public List<NavModel> Children { get; set; }

        public NavModel(int id, string? href, string icon, string title, List<NavModel>? children)
        {
            Id = id;
            Href = href;
            Icon = icon;
            ParentIcon = icon;
            Title = title;
            FullTitle = title;
            Children = children;
        }
        public NavModel()
        {

        }

    }
}
