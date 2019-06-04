using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioStreaming
{
    public partial class PlayMusic : Form
    {
        WaveChannel32 waveChannel32Receive;
        string _mtenbaihat;
        public PlayMusic(WaveChannel32 waveChannel32 ,string tenbaihat)
        {
            InitializeComponent();
            this.waveChannel32Receive = waveChannel32;
            _mtenbaihat = tenbaihat;
        }
        DirectSoundOut directSoundOutReceive;
        private void PlayMusic_Load(object sender, EventArgs e)
        {
            lblTenBaiHat.Text = _mtenbaihat;
            directSoundOutReceive = new DirectSoundOut();
            directSoundOutReceive.Volume = 1.0f;
            directSoundOutReceive.Init(waveChannel32Receive);
            directSoundOutReceive.Play();
            
        }

        private void PlayMusic_FormClosing(object sender, FormClosingEventArgs e)
        {
            directSoundOutReceive.Stop();
        }
    }
}
