﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace mqcl
{
    public partial class Form1 : Form
    {
        private MqttClient client;
        public Form1()
        {
            InitializeComponent();
            //创建客户端实例  
           // client = new MqttClient(IPAddress.Parse("119.29.9.48"));
           //  client = new MqttClient(IPAddress.Parse("115.29.111.161")); //主机为IP时  
           //client = new MqttClient("www.difiot.com"); //当主机地址为域名时  

            // 注册消息接收处理事件，还可以注册消息订阅成功、取消订阅成功、与服务器断开等事件处理函数  
           // client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
          

            //生成客户端ID并连接服务器  
            // string clientId = Guid.NewGuid().ToString();
            //string clientId = "21122111222";
           // client.Connect(clientId);
            //client.Connect(clientId, "tang", "88888888");
           // client.Connect(clientId, "admin", "admin", true, 1, true, "G/oo", "kill...", true, 200);
           // client.Connect(clientId, "admin", "admin");
            // 订阅主题"/home/temperature" 消息质量为 2   
            //client.Subscribe(new string[] { "CarTrack Report" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
           // client.Subscribe(new string[] { "substopic" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //client.Subscribe(new string[] { "/home/humidity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //client.Subscribe(new string[] { "G/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
   
  
        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //处理接收到的消息  
            string msg = System.Text.Encoding.Default.GetString(e.Message);
            
             if (this.InvokeRequired)
             {
                 MqttClient.MqttMsgPublishEventHandler setpos = new MqttClient.MqttMsgPublishEventHandler(client_MqttMsgPublishReceived);
                 this.Invoke(setpos, new object[] { sender },  e );
             }
             else
             {
                 richTextBox2.AppendText(e.Topic+":" + msg + "--"+DateTime.Now.ToString()+"\r\n"); 
             }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 发布消息到主题 "/home/temperature" 消息质量为 2,不保留   
            //client.Publish("G/www", Encoding.UTF8.GetBytes(richTextBox1.Text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);   
            client.Publish(pubtoc.Text , Encoding.UTF8.GetBytes(richTextBox1.Text), 2, false);

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void but_sub_Click(object sender, EventArgs e)
        {
            client.Subscribe(new string[] { subtoc.Text }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            MessageBox.Show("订阅OK");
        }

        private void but_connet_Click(object sender, EventArgs e)
        {
            client = new MqttClient(IPAddress.Parse(ip.Text ));
            //  client = new MqttClient(IPAddress.Parse("115.29.111.161")); //主机为IP时  
            //client = new MqttClient("www.difiot.com"); //当主机地址为域名时  

            // 注册消息接收处理事件，还可以注册消息订阅成功、取消订阅成功、与服务器断开等事件处理函数  
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


            //生成客户端ID并连接服务器  
            string clientId = Guid.NewGuid().ToString();
            //string clientId = "21122111222";
            client.Connect(clientId);
            MessageBox.Show("连接OK");
        }  
    }
}
