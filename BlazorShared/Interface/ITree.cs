using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorXT.Interface
{
    public interface ITree<T>
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentId { get; }
        /// <summary>
        /// 孩子节点
        /// </summary>
        public List<T> Children { get; set; }
    }
}
