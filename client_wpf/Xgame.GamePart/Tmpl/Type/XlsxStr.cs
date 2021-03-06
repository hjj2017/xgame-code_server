﻿using System;

namespace Xgame.GamePart.Tmpl.Type
{
    /// <summary>
    /// Xlsx 字符串型
    /// </summary>
    public class XlsxStr : PrimitiveTypeCol<string>
    {
        #region 类构造器
        /// <summary>
        /// 类默认构造器
        /// </summary>
        public XlsxStr()
        {
        }

        /// <summary>
        /// 类参数构造器
        /// </summary>
        /// <param name="val"></param>
        public XlsxStr(string val)
        {
            this.Val = val;
        }
        #endregion

        // @Override
        protected override void ReadImpl(XlsxRowReadStream stream)
        {
            this.Val = stream.ReadStr();
        }
    }
}
