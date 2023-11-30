using System;
using System.Collections.Generic;
using System.Text;

namespace Client.API.Models
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class OwnPersonDto
    {
        /// <summary>
        ///姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 曾用名
        /// </summary>
        public string UsedName { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 派出所
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 道路
        /// </summary>
        public string Road { get; set; }

        /// <summary>
        /// 门牌号
        /// </summary>
        public string Door { get; set; }



        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 户籍姓名
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 户籍身份证
        /// </summary>
        public string LeaderIDCard { get; set; }


        /// <summary>
        /// 户籍身份证
        /// </summary>
        public string LeaderRelation { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }
    }
}
