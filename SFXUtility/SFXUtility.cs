﻿#region License

/*
 Copyright 2014 - 2014 Nikita Bernthaler
 SFXUtility.cs is part of SFXUtility.
 
 SFXUtility is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 SFXUtility is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with SFXUtility. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

namespace SFXUtility
{
    #region

    using System;
    using Class;
    using LeagueSharp;
    using LeagueSharp.Common;

    #endregion

    internal class SFXUtility
    {
        #region Constructors

        public SFXUtility()
        {
            try
            {
                Menu = new Menu("SFXUtility-浜岀嫍姹夊寲", "SFXUtility", true);

                var miscMenu = new Menu("鏉傞」", "Misc");

                var infoMenu = new Menu("淇℃伅", "Info");
                infoMenu.AddItem(new MenuItem("InfoVersion", string.Format("鐗堟湰鍙穦: {0}", Version)));
                infoMenu.AddItem(new MenuItem("InfoIRC", "IRC缃戠粶鑱婂ぉ瀹: Appril"));
                infoMenu.AddItem(new MenuItem("InfoGithub", "Github:").SetValue(new StringList(new[]
                {
                    "Smokyfox",
                    "Lizzaran"
                })));

                miscMenu.AddSubMenu(infoMenu);

                miscMenu.AddItem(new MenuItem("MiscCircleThickness", "绾垮湀鍘氬害").SetValue(new Slider(3, 10, 1)));

                Menu.AddSubMenu(miscMenu);
                AppDomain.CurrentDomain.DomainUnload += OnExit;
                AppDomain.CurrentDomain.ProcessExit += OnExit;
                CustomEvents.Game.OnGameEnd += OnGameEnd;
                Game.OnGameEnd += OnGameEnd;
                CustomEvents.Game.OnGameLoad += OnGameLoad;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Events

        public event EventHandler OnUnload;

        #endregion

        #region Properties

        public Menu Menu { get; private set; }

        public string Name
        {
            get { return "SFXUtility"; }
        }

        public Version Version
        {
            get { return new Version(0, 7, 6, 0); }
        }

        #endregion

        #region Methods

        private void OnExit(object sender, EventArgs e)
        {
            try
            {
                EventHandler handler = OnUnload;
                if (null != handler) handler(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void OnGameEnd(EventArgs args)
        {
            try
            {
                OnExit(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void OnGameLoad(EventArgs args)
        {
            try
            {
                Chat.Print(string.Format("{0} v{1}.{2}.{3} 鍔犺浇鎴愬姛!姹夊寲by浜岀嫍!QQ缇361630847..", Name, Version.Major, Version.Minor, Version.Build));
                Menu.AddToMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}