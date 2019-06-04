using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioStreaming
{
    public partial class frmMain : Form
    {
        enum SendType
        {
            ENDSTREAMING,
            STREAMING,
            AUDIO_DATA,
            REQUEST_ERROR,
            CURRENT_POSSITION,
            PAUSE,
            RESUME,
            GET_AUDIO_DATA,
            GET_CURRENT_POSSITION,
        }

        class Message
        {
            public SendType TypeSend;
            public string From;
            public string To;
            public byte[] Data;
            public string DataExtension;
        }

      
        class StreamPlaying
        {
            public string Source { get; set; }
            public string MusicName { get; set; }

        }
        List<StreamPlaying> streamPlayings;
        // network
        TcpClient tcpClient;
        NetworkStream networkStream;

        // audio stream receive
        MemoryStream mp3StreamMemoryReceive;
        Mp3FileReader mp3FileReaderReceive;
        WaveChannel32 waveChannel32Receive;
        DirectSoundOut directSoundOutReceive;

        // audio stream

        Mp3FileReader mp3Reader;
        WaveChannel32 waveChannel32;
        DirectSoundOut directSoundOut;
        WaveOut waveOut;

        public frmMain()
        {
            InitializeComponent();
            streamPlayings = new List<StreamPlaying>();
            directSoundOutReceive = new DirectSoundOut();

        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient(txtIPServer.Text, 9009);
                if (tcpClient.Connected)
                {
                    MessageBox.Show("Kết nối thành công");
                }
                else
                {
                    MessageBox.Show("Kết nối tới server thất bại");
                    return;
                }
                networkStream = tcpClient.GetStream();
                Thread t = new Thread(ReceivedDataFromServer);
                t.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        bool eventAudioData;
        private void ReceivedDataFromServer()
        {
            try
            {
                while (true)
                {    
                    var buff = new byte[11000000];
                    int recv = networkStream.Read(buff, 0, buff.Length);
                    string stringdata = Encoding.ASCII.GetString(buff, 0, recv);
                    var jsondata = JsonConvert.DeserializeObject<Message>(stringdata);
                    if (jsondata.From != tcpClient.Client.LocalEndPoint.ToString())
                    {
                        switch (jsondata.TypeSend)
                        {
                            case SendType.STREAMING:
                                {
                                    lsvDanhSachDangPhat.Invoke((MethodInvoker)(() =>
                                    {
                                       
                                        ListViewItem listViewItem = new ListViewItem();
                                        listViewItem.Name = jsondata.From;
                                        listViewItem.Text = jsondata.From;
                                        listViewItem.SubItems.Add(jsondata.DataExtension);
                                        lsvDanhSachDangPhat.Items.Add(listViewItem);
                                    }));
                                    break;
                                }

                            case SendType.AUDIO_DATA:
                                {
                                    mp3StreamMemoryReceive = new MemoryStream(jsondata.Data);
                                    mp3FileReaderReceive = new Mp3FileReader(mp3StreamMemoryReceive);
                                    waveChannel32Receive = new WaveChannel32(mp3FileReaderReceive);
                                    eventAudioData = true;
                                    break;
                                }
                            case SendType.GET_AUDIO_DATA:
                                {
                                    var datasend = new Message
                                    {
                                        TypeSend = SendType.AUDIO_DATA,
                                        From = tcpClient.Client.LocalEndPoint.ToString(),
                                        To = jsondata.From,
                                        Data = File.ReadAllBytes(txtNoiDung.Text),
                                    };
                                    var jsondatasend = JsonConvert.SerializeObject(datasend);
                                    var jsonbytesend = Encoding.ASCII.GetBytes(jsondatasend);
                                    networkStream.Write(jsonbytesend, 0, jsondatasend.Length);
                                    break;
                                }
                            case SendType.GET_CURRENT_POSSITION:
                                {
                                    var datasend = new Message
                                    {
                                        TypeSend = SendType.CURRENT_POSSITION,
                                        From = tcpClient.Client.LocalEndPoint.ToString(),
                                        To = jsondata.From,
                                        Data = Encoding.ASCII.GetBytes((waveChannel32.CurrentTime.TotalSeconds).ToString()),
                                    };

                                    var jsondatasend = JsonConvert.SerializeObject(datasend);
                                    var jsonbytesend = Encoding.ASCII.GetBytes(jsondatasend);
                                    networkStream.Write(jsonbytesend, 0, jsondatasend.Length);
                                    break;
                                }

                            case SendType.PAUSE:
                                {
                                    directSoundOutReceive.Pause();

                                    break;
                                }

                            case SendType.RESUME:
                                {
                                    directSoundOutReceive.Play();
                                    break;
                                }

                            case SendType.CURRENT_POSSITION:
                                {
                                    var s = Encoding.ASCII.GetString(jsondata.Data, 0, jsondata.Data.Length);
                                    double senconds = double.Parse(s);

                                  
                                    waveChannel32Receive.CurrentTime = TimeSpan.FromSeconds(senconds);
                                    eventCurrentPossition = true;
                                    break;
                                }
                            case SendType.ENDSTREAMING:
                                {
                                    var lsvitem = new ListViewItem();
                                    lsvitem.Name = jsondata.From;
                                    //lsvDanhSachDangPhat.Invoke((MethodInvoker)(() =>
                                    //{

                                    //    lsvDanhSachDangPhat.Items.Remove(lsvitem);
                                    //}));
                                    RemoveFromClientList(jsondata.From);
                                    lblStatus.Invoke((MethodInvoker)(() =>
                                    {
                                        lblStatus.Text = "";
                                    }));


                                    directSoundOutReceive.Stop();
                                    break;
                                }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSend_Click(object sender, EventArgs e)
        {

            try
            {
                if(waveOut != null)
                {
                    waveOut.Stop();
                    var datasendo = new Message
                    {
                        TypeSend = SendType.ENDSTREAMING,
                        From = tcpClient.Client.LocalEndPoint.ToString(),
                        DataExtension = Path.GetFileName(txtNoiDung.Text),
                    };

                    var jsondatao = JsonConvert.SerializeObject(datasendo);
                    var buffo = Encoding.ASCII.GetBytes(jsondatao);

                    networkStream.Write(buffo, 0, jsondatao.Length);
                }
                mp3Reader = new Mp3FileReader(txtNoiDung.Text);

                waveChannel32 = new WaveChannel32(mp3Reader);
                waveOut = new WaveOut();
                waveOut.Init(waveChannel32);
                waveOut.Volume = 0.1f;





                var datasend = new Message
                {
                    TypeSend = SendType.STREAMING,
                    From = tcpClient.Client.LocalEndPoint.ToString(),
                    DataExtension = Path.GetFileName(txtNoiDung.Text),
                };

                var jsondata = JsonConvert.SerializeObject(datasend);
                var buff = Encoding.ASCII.GetBytes(jsondata);

                networkStream.Write(buff, 0, jsondata.Length);
                waveOut.Play();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          

        }

       
        private void btnMoNhac_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtNoiDung.Text = openFileDialog1.FileName;
               
            }
        }

        bool ispause = false;
        private void btnPause_Click(object sender, EventArgs e)
        {
           
            try
            {
                if(directSoundOutReceive != null)
                {
                    if(directSoundOutReceive.PlaybackState == PlaybackState.Playing)
                    {
                        directSoundOutReceive.Stop();
                    }
                    
                }
                
                if(waveOut == null)
                {
                    return;
                } 
                if (waveOut.PlaybackState != PlaybackState.Playing)
                {
                    return;
                }

                SendType st;
                waveOut.Stop();
                
                //if (ispause)
                //{
                //    waveOut.Play();
                //    btnPause.Text = "Pause";
                //    st = SendType.RESUME;
                //    ispause = false;
                //}
                //else
                //{
                //    waveOut.Pause();
                //    btnPause.Text = "Resume";
                //    st = SendType.PAUSE;
                //    ispause = true;
                //}

                var datasend = new Message
                {
                    TypeSend = SendType.ENDSTREAMING,
                    From = tcpClient.Client.LocalEndPoint.ToString(),
                    DataExtension = Path.GetFileName(txtNoiDung.Text),
                };

                var jsondata = JsonConvert.SerializeObject(datasend);
                var buff = Encoding.ASCII.GetBytes(jsondata);

                networkStream.Write(buff, 0, jsondata.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           
        }
        bool eventCurrentPossition;
        bool inProgess = false;
        private void lsvDanhSachDangPhat_SelectedIndexChanged(object sender, EventArgs e)
        {
 

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(waveOut != null)
            {
                waveOut.Stop();
                var datasend = new Message
                {
                    TypeSend = SendType.ENDSTREAMING,
                    From = tcpClient.Client.LocalEndPoint.ToString(),
                    DataExtension = Path.GetFileName(txtNoiDung.Text),
                };

                var jsondata = JsonConvert.SerializeObject(datasend);
                var buff = Encoding.ASCII.GetBytes(jsondata);

                networkStream.Write(buff, 0, jsondata.Length);

            }
            Application.Exit();
           

        }
        PlayMusic playMusic;
        private void lsvDanhSachDangPhat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (inProgess)
            {
                MessageBox.Show("đang tải bộ đệm, xin chờ");
                return;
            }
            var item = lsvDanhSachDangPhat.SelectedItems[0];
            var getdata = new Message
            {
                From = tcpClient.Client.LocalEndPoint.ToString(),
                To = item.Text,
                TypeSend = SendType.GET_AUDIO_DATA,
            };
            var getdatajson = JsonConvert.SerializeObject(getdata);
            var buff = Encoding.ASCII.GetBytes(getdatajson);
            networkStream.Write(buff, 0, getdatajson.Length);
            lblStatus.Text = "Đang tải bộ đệm";
            inProgess = true;
            while (true)
            {
                if (eventAudioData == true)
                {
                    eventAudioData = false;
                    break;
                }
            }
            eventAudioData = false;
            getdata = new Message
            {
                From = tcpClient.Client.LocalEndPoint.ToString(),
                To = item.Text,
                TypeSend = SendType.GET_CURRENT_POSSITION,
            };

            getdatajson = JsonConvert.SerializeObject(getdata);
            buff = Encoding.ASCII.GetBytes(getdatajson);
            networkStream.Write(buff, 0, getdatajson.Length);
            lblStatus.Text = "Đang đồng bộ";
            while (true)
            {
                if (eventCurrentPossition == true)
                {
                    eventCurrentPossition = false;
                    lblStatus.Invoke((MethodInvoker)(() =>
                    {
                        lblStatus.Text = "Bài đang phát " + item.SubItems[1].Text;
                    }));
                   
                    directSoundOutReceive.Volume = 1.0f;
                    directSoundOutReceive.Init(waveChannel32Receive);
                    directSoundOutReceive.Play();
                    //playMusic = new PlayMusic(waveChannel32Receive, item.SubItems[1].Text);
                    //playMusic.ShowDialog();
                    //playMusic.Invoke((MethodInvoker)(() => {
                    //    if (!playMusic.Visible)
                    //    {
                    //        playMusic.ShowDialog();
                    //    }

                    //}));
                    break;
                }
            }
            inProgess = false;
          
        }

        public ListViewItem GetItemtoDelete(string ClientName)
        {
            ListViewItem listviewitem = new ListViewItem();
            for (int i = 0; i < lsvDanhSachDangPhat.Items.Count; i++)
            {
                listviewitem = lsvDanhSachDangPhat.Items[i];
                if (ClientName == listviewitem.Text)
                {
                    return listviewitem;
                }
            }
            return null;
        }


        public void RemoveFromClientList(string ClientName)
        {
            ListViewItem listviewitem = new ListViewItem();
            listviewitem = GetItemtoDelete(ClientName);
            if (listviewitem != null)
            {
                //if (InvokeRequired)
                //{
                    Invoke((MethodInvoker)delegate () { lsvDanhSachDangPhat.Items.Remove(listviewitem); });
                //}
                //else
                //{
                //    lsvDanhSachDangPhat.Items.Remove(listviewitem);
                //}
            }
        }
    }
}
