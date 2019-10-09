﻿using System;
using System.Collections.Generic;
using System.Text;


using Xamarin.Forms;
using FFImageLoading.Forms;
using System.Timers;

namespace INFT2051app {
    class Egg : CachedImage{
        /*
         * The Egg Class specifies which type the egg is (LAND, SEA, AIR)
         * The player could either choose which egg they want or get an egg randomly
         * For now the egg is generated randomly
         * 
         */

        public event EventHandler Hatch;
        private Timer EggTimer;
        string Type { get; set; } //TYPE OF EGG (LAND, SEA, AIR)
        string SpritePath { get; set; } // SPRITE PATH OF EGG

        public Egg() {
            Type = "Land";
            SpritePath = "";
            Source = "egg_brown.png";

            Hatch += OnHatch;

            //Image properties
            
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0.5, 0.8, 0.2, 0.2));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);

            EggTimer = new Timer();
            EggTimer.Elapsed += OnHatch;
        }

        public void StartTimer() {
            EggTimer.Start();
        }

        public void OnHatch(object sender, EventArgs e) {
            Hatch(this, EventArgs.Empty);
            EggTimer.Stop();

        }
    }
}
