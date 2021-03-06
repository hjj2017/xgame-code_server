﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Xgame.GameBizModule;
using Xgame.GameBizModule.CreateHuman.BizServ;
using Xgame.GameBizModule.CreateHuman.Tmpl;
using Xgame.GameBizModule.Human.Msg;
using Xgame.GameClient.Msg;
using Xgame.GamePart.Msg.Type;

namespace Xgame.WpfApp.Human
{
    /// <summary>
    /// Page_CreateHuman.xaml 的交互逻辑
    /// </summary>
    public partial class Page_CreateHuman : Page
    {
        /** 创建角色模版列表 */
        private IList<CreateHumanTmpl> _tmplList = null;
        /** 选择索引 */
        private int _selIndex = 0;
        /** 图片数组 */
        private BitmapImage[] _bitmapArr = null;

        /// <summary>
        /// 类默认构造器
        /// </summary>
        public Page_CreateHuman()
        {
            // 初始化控件
            this.InitializeComponent();
            // 获取创建角色模版列表
            this._tmplList = new List<CreateHumanTmpl>(CreateHumanServ.OBJ.GetCreateHumanTmplList());
            // 创建图片数组
            this._bitmapArr = new BitmapImage[this._tmplList.Count];

            // 设置默认的角色名
            this._txtName.Text = Player.OBJ._userName;
        }

        /// <summary>
        /// 选择前一个武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnPrev_Click(object sender, RoutedEventArgs e)
        {
            this._selIndex--;
            this.UpdateHeroImg();
        }

        /// <summary>
        /// 选择后一个武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnNext_Click(object sender, RoutedEventArgs e)
        {
            this._selIndex++;
            this.UpdateHeroImg();
        }

        /// <summary>
        /// 更新英雄图片
        /// </summary>
        private void UpdateHeroImg()
        {
            if (this._selIndex < 0)
            {
                // 如果索引位置已经到开头, 
                // 那么首尾相接
                this._selIndex = this._tmplList.Count - 1;
            }

            if (this._selIndex > this._tmplList.Count - 1)
            {
                // 如果索引位置已经到末尾, 
                // 那么从头再来
                this._selIndex = 0;
            }

            // 获取创建角色模版
            CreateHumanTmpl selTmpl = this._tmplList[this._selIndex];

            if (selTmpl == null)
            {
                // 如果模版对象为空, 
                // 则直接退出!
                return;
            }

            // 获取当前图片
            BitmapImage currBitmap = this._bitmapArr[this._selIndex];

            if (currBitmap == null)
            {
                // 如果图片为空, 
                currBitmap = new BitmapImage();
                currBitmap.BeginInit();
                // 更新武将图片
                currBitmap.UriSource = new Uri(
                    "../Res/Img/CreateHuman/" + selTmpl.HeroImgName,
                    UriKind.Relative
                );
                currBitmap.EndInit();
            }

            // 更新武将图片
            this._imgHero.Source = currBitmap;
        }

        /// <summary>
        /// 选定武将并创建角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnOk_Click(object sender, RoutedEventArgs e)
        {
            string humanName = this._txtName.Text;

            CGCreateHuman cgMSG = new CGCreateHuman();
            cgMSG._heroTmplId = new MsgInt(1001);
            cgMSG._humanName = new MsgStr(humanName);
            cgMSG._serverName = new MsgStr(Player.OBJ._selectedServerName);

            ClientServer.OBJ.AddGCMsgHandler<GCCreateHuman>(this.Handle_GCCreateHuman);
            ClientServer.OBJ.SendCGMsg(cgMSG);
        }

        /// <summary>
        /// 处理 GCCreateHuman 消息
        /// </summary>
        /// <param name="gcMSG"></param>
        private void Handle_GCCreateHuman(GCCreateHuman gcMSG)
        {
            // 创建角色成功之后, 
            // 尝试进入角色
            this.Dispatcher.BeginInvoke(new Action(() => { MainWindow.TheWnd.Content = new Page_EnterHumanAndLoading(); }));
        }
    }
}
