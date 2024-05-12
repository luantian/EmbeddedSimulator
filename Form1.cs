using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

// COM1：屏幕串口通讯,115200，命令格式见手册，对每个id值进行赋值、高亮、隐藏、界面切换。
// COM2：电阻输出卡通讯，115200，命令1：AT+RES.SP=123.45/ 控制输出123.45Ω；命令2：AT+DEVRELAY_LOG?/ 查询继电器使用次数（自检用途）
// 调试区按键，最终对应一个按键返回的事件，返回值0~24，调试时用软件按钮，实物调试时可尽快对应。
// 按键说明1：当设置按下时，数值输入按键才有效，否则数值输入忽略；数值输入后，取消返回原值，确定写入新值。当输入新值，且输出=True，直接写入串口2；当输出按键False—>True，写入串口2
// 未做 按键说明2：上下左右，会移动光标（或一个选择框）在测量概览、输出概览、自检的7行信息进行跳转，当按确认键时，切换到对应选择的行。
// 按键说明3：上页、下页；当光标在左侧输入区时，切换输入显示行，并高亮显示当前的行；当光标在输出侧，切换输出显示行，并高亮。在自检行，跳转到自检页面。
// 按键说明4：模式按键，切换电阻测量的模式，有3种：二线模式、三线模式、四线模式；并切换电阻测量的函数设置，有2/3/4线模式设置。屏幕对应显示当前模式（只在电阻测量显示）其他测量只有二线模式。
// 按键说明5：主页，就是切换到测量页面；测量、输出按键，是启动当前上方显示的当前通道的启动、停止。
// 特殊说明：

namespace EmbeddedSimulator
{
    public partial class Form1 : Form
    {
        Keyboard keyboard;
        //HardwareBoard hardboard;

        public Form1()
        {
            InitializeComponent();
            keyboard = new Keyboard(COM1);
            //hardboard = new HardwareBoard(COM2);

            //hardboard.SelfTest();
        }
        private void Num_0_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_0.Text);
        }

        private void Num_1_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_1.Text);
        }

        private void Num_2_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_2.Text);
        }

        private void Num_3_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_3.Text);
        }

        private void Num_4_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_4.Text);
        }

        private void Num_5_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_5.Text);
        }

        private void Num_6_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_6.Text);
        }

        private void Num_7_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_7.Text);
        }

        private void Num_8_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_8.Text);
        }

        private void Num_9_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_9.Text);
        }

        private void Num_P_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_P.Text);
        }

        private void Num_Cancel_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_Cancel.Text);
        }

        private void Num_Set_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_Set.Text);
        }

        private void Num_Sure_Click(object sender, EventArgs e)
        {
            OnClickButton(Num_Sure.Text);
        }

        private void Dir_UP_Click(object sender, EventArgs e)
        {
            OnClickButton(Dir_UP.Text);
        }

        private void Dir_DOWN_Click(object sender, EventArgs e)
        {
            OnClickButton(Dir_DOWN.Text);
        }

        private void Dir_LEFT_Click(object sender, EventArgs e)
        {
            OnClickButton(Dir_LEFT.Text);
        }

        private void Dir_RIGHT_Click(object sender, EventArgs e)
        {
            OnClickButton(Dir_RIGHT.Text);
        }

        private void Tab_Up_Click(object sender, EventArgs e)
        {
            OnClickButton(Tab_Up.Text);
        }

        private void Tab_Down_Click(object sender, EventArgs e)
        {
            OnClickButton(Tab_Down.Text);
        }
        private void Ctrl_Output_Click(object sender, EventArgs e)
        {
            OnClickButton(Ctrl_Output.Text);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            OnClickButton(deleteButton.Text);
        }
        private void Tab_Main_Click(object sender, EventArgs e)
        {
            // 点击主页按钮，切换到测量页面
            OnClickButton(Tab_Main.Text);
        }

        private void Ctrl_Mode_Click(object sender, EventArgs e)
        {

            // TODO 发送 切换模式
            //SendData1();
            OnClickButton(Ctrl_Mode.Text);
        }

        private void Ctrl_Input_Click(object sender, EventArgs e)
        {
            // TODO 点击测量 500ms读取一次数据
            OnClickButton(Ctrl_Input.Text);
        }

        /// 串口1接收数据事件，当接受到数据时候自动触发事件
        public void COM1DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        /// 串口2接收数据事件，当接受到数据时候自动触发事件
        public void COM2DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void OnClickButton(string ButtonText)
        {
            byte[] command = new byte[1];
            switch (ButtonText)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ".":
                    keyboard.ZeroToPoint(ButtonText);
                    break;
                case "←":
                    keyboard.Delete();
                    break;
                case "取消":
                    keyboard.Cancel();
                    break;
                case "确定":
                    keyboard.Sure();
                    break;
                case "模式":
                    keyboard.ChangeMode();
                    break;
                case "设置":
                    keyboard.Set();
                    break;
                case "上":
                    keyboard.Up();
                    break;
                case "下":
                    keyboard.Down();
                    break;
                case "左":
                    keyboard.Left();
                    break;
                case "右":
                    keyboard.Right();
                    break;
                case "上页":
                    keyboard.PrevPage();
                    break;
                case "下页":
                    keyboard.NextPage();
                    
                    break;
                case "主页":
                    keyboard.Router("01");
                    break;
                case "输出":
                    //GenerateRandomNumber();
                    keyboard.Output();
                    break;
                case "测量":
                    //keyboard.SelfPage00();
                    keyboard.Test();
                    // keyboard.Input();
                    break;
                default:
                    break;
            }
        }

        
    }

    class Utils
    {
        public static byte[] StringToByteArray(string hexString)
        {
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        // 删除字符串最后一位
        public static string RemoveLastCharacter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.Substring(0, input.Length - 1);
        }

        // 将明文字符串转成16进制
        public static string StringToHex(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            string hexString = BitConverter.ToString(bytes).Replace("-", "");
            return hexString;
        }

        // 随机生成0~1000，并保留3位小数的数据
        public static double GenerateRandomNumber()
        {
            // 创建 Random 对象
            Random random = new Random();

            // 生成0到1000的随机数
            double randomNumber = random.NextDouble() * 1000;

            return Math.Round(randomNumber, 3);
        }

        // 将双精度浮点数转换为十六进制字符串
        public static string DoubleToHex(double value)
        {
            long longValue = BitConverter.DoubleToInt64Bits(value); // 将双精度浮点数转换为长整型
            return longValue.ToString("X16"); // 将长整型转换为十六进制字符串
        }
    }

    class PortData
    {
        public string Address { set; get; }
        public string Value { set; get; }
    }

    class Range
    {
        public int Min { set; get;}
        public int Max { set; get;}
    }

    class Keyboard
    {
        SerialPort Com;
        string Num = "";
        string OldNum = "";
        string PortHeader = "5AA5";  // 串口 帧头
        string WriteCode = "82"; // 串口 写
        string ReadCode = "83";  // 串口 读
        int Delay = 50;
        static string Open = "0001";
        static string Close = "0000";

        // 自检页面 六条自检通道变量地址 1001 - 1006
        static string SelfPage00PageAddress = "1001";

        // 输出
        static string InputAddress = "6000";  // 当前测量值 变量地址
        static string InputUnitAddress = "2001"; // 当前测量值 单位 变量地址
        static string InputOnOffIconAddress = "2010"; // 当前测量值 测量启停图标 变量地址
        static string InputRangeAddress = "2020"; // 输入/测量 量程 变量地址

        static string OutputAddress = "8000";  // 当前输出值 变量地址
        static string OutputUnitAddress = "3001"; // 当前测量值 单位 变量地址
        static string OutputOnOffIconAddress = "3010"; // 当前测量值 测量启停图标 变量地址
        static string OutputRange = "3020"; // 输入/测量 量程 变量地址

        // 概览区域 变量地址 开始

        static string ResistanceInputModeAddress = "2030";  // 电阻模式 变量地址
        static string PotentiometerInputModeAddress = "2031";  // 电位差计模式 变量地址
        static string VoltageInputModeAddress = "2032";  // 电压模式 变量地址
        static string ElectricCurrentInputModeAddress = "2033";  // 电流模式 变量地址
        static string ResistanceOutModeAddress = "3030";  // 电阻模式 变量地址
        static string PotentiometerOutModeAddress = "3031";  // 电位差计模式 变量地址

        List<string> OverviewAddressList = new List<string> { ResistanceInputModeAddress, PotentiometerInputModeAddress, VoltageInputModeAddress, ElectricCurrentInputModeAddress, ResistanceOutModeAddress, PotentiometerOutModeAddress };

        static string ResistanceInputActiveModeAddress = "2130";  // 电阻模式 选中状态 变量地址
        static string PotentiometerInputActiveModeAddress = "2131";  // 电位差计模式 选中状态 变量地址
        static string VoltageInputActiveModeAddress = "2132";  // 电压模式 选中状态 变量地址
        static string ElectricCurrentInputActiveModeAddress = "2133";  // 电流模式 选中状态 变量地址
        static string ResistanceOutActiveModeAddress = "2134";  // 电阻模式 选中状态 变量地址
        static string PotentiometerOutActiveModeAddress = "2135";  // 电位差计模式 选中状态 变量地址
        static string SelfTestRouterAddress = "2136"; // 自检状态 选中状态 变量地址

        static string ResistanceInputModeOverviewAddress = "6100";  // 电阻模式 变量地址
        static string PotentiometerInputModeOverviewAddress = "6200";  // 电位差计模式 变量地址
        static string VoltageInputModeOverviewAddress = "6300";  // 电压模式 变量地址
        static string ElectricCurrentInputModeOverviewAddress = "6400";  // 电流模式 变量地址
        static string ResistanceOutModeOverviewAddress = "8100";  // 电阻模式 变量地址
        static string PotentiometerOutModeOverviewAddress = "8200";  // 电位差计模式 变量地址

        static string SelfTest01PageAddress = "4020"; // 4021

        // 概览区域 变量地址 结束

        int ModeIndex = 0;  // 模式索引
        PortData[] ModeList = {
            new PortData { Address = ResistanceInputModeAddress, Value = "0000" },
            new PortData { Address = ResistanceInputModeAddress, Value = "0001" },
            new PortData { Address = ResistanceInputModeAddress, Value = "0002" },
        };

        int ChannelIndex = 0;
        string[] ChannelAddress = {
            ResistanceInputActiveModeAddress,
            PotentiometerInputActiveModeAddress,
            VoltageInputActiveModeAddress,
            ElectricCurrentInputActiveModeAddress,
            ResistanceOutActiveModeAddress,
            PotentiometerOutActiveModeAddress,
            SelfTestRouterAddress
        };
        string[] ChannelOverviewAddress =
        {
            ResistanceInputModeOverviewAddress,
            PotentiometerInputModeOverviewAddress,
            VoltageInputModeOverviewAddress,
            ElectricCurrentInputModeOverviewAddress,
            ResistanceOutModeOverviewAddress,
            PotentiometerOutModeOverviewAddress
        };


        PortData[] InputRangeList = {
            new PortData { Address = InputRangeAddress, Value = "0000" },
            new PortData { Address = InputRangeAddress, Value = "0001" },
            new PortData { Address = InputRangeAddress, Value = "0002" },
            new PortData { Address = InputRangeAddress, Value = "0003" },
            new PortData { Address = OutputRange, Value = Close },
            new PortData { Address = OutputRange, Value = Open },
        };

        Range[] RangeList = {
            new Range { Min = 1, Max = 1000 },
            new Range { Min = 0, Max = 200 },
            new Range { Min = 0, Max = 10 },
            new Range { Min = 0, Max = 20 },
            new Range { Min = 1, Max = 1000 },
            new Range { Min = 0, Max = 200 },
        };

        enum Status
        {
            Init,
            Cutover,
            Setting,
        }

        Boolean InputStatus = false;
        Boolean OutputStatus = false;

        Status CurrentStatus = Status.Init;  // 0 => 初始状态  // 1 => 上下左右切换状态  // 2 => 点击了设置按钮，可输入数字的状态



        public Keyboard(SerialPort COM) {
            Com = COM;
            OpenCom();
        }

        private void OpenCom()
        {
            Com.PortName = "COM4";
            Com.BaudRate = Convert.ToInt32("115200");
            Com.ReceivedBytesThreshold = 1;
            Com.Open();
        }

        public async void Sure()
        {
            if (!Com.IsOpen)
            {
                return;
            }
            switch(CurrentStatus)
            {
                case Status.Init:
                    break;
                case Status.Cutover:
                    // 切换通道，并启动当前通道
                    // 切换到最后一个通道，也就是自检通道，点击确定 跳转到00页
                    if (ChannelIndex == ChannelAddress.Length - 1)
                    {
                        Router("00");
                        break;
                    }
                    SendChangeVarIcon(InputRangeList[ChannelIndex]);
                    await Task.Delay(Delay);
                    OpenChannel(ChannelIndex);
                    break;
                case Status.Setting:
                    /*
                        输入数据后，确认
                        1. 判断输入的数据是否在量程范围之内，如果不在将数据清空
                        2. 当前 启动 输出 ，直接写入，未启动点击输出按钮 再写入 硬件
                     */
                    if (Num == "")
                    {
                        break;
                    }
                    double result = double.Parse(Num);
                    Range range = RangeList[ChannelIndex];
                    if (result > range.Max || result < range.Min)
                    {
                        // 输入的数据，不在量程范围内，清空当前数据
                        Clear();
                        Num = "";
                        break;
                    } else
                    {
                        // 代码执行此处，说明输入的值合法（在量程范围内）
                        // 如果输出状态 处于开启 立刻发送数据给硬件
                        if (OutputStatus)
                        {
                            // TODO 发送数据给硬件
                        }
                        OldNum = Num;
                    }
                    break;
            }
            CurrentStatus = Status.Init;
        }

        public void ZeroToPoint(string Val)
        {
            if (CurrentStatus == Status.Setting)
            {
                Num += Val;
                SendOutputNum(Num);
            }
        }

        public void Up()
        {
            if (CurrentStatus == Status.Setting)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            string direct = "";
            if (ChannelIndex == 0)
            {
                ChannelIndex = ChannelAddress.Length - 1;
            }
            else
            {
                ChannelIndex--;
            }
            for (int i = 0; i < ChannelAddress.Length; i++)
            {
                direct += ChannelIndex == i ? Open : Close;
            }
            PortData portData = new PortData{ Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }
        public void Down()
        {
            if (CurrentStatus == Status.Setting)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            string direct = "";
            if (ChannelIndex >= ChannelAddress.Length - 1)
            {
                ChannelIndex = 0;
            }
            else
            {
                ChannelIndex++;
            }
            for (int i = 0; i < ChannelAddress.Length; i++)
            {
                direct += ChannelIndex == i ? Open : Close;
            }
            PortData portData = new PortData { Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }
        public void Left()
        {
            if (CurrentStatus == Status.Setting)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            string direct = Open + Close + Close + Close + Close + Close + Close;
            ChannelIndex = 0;
            PortData portData = new PortData { Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }
        public void Right()
        {
            if (CurrentStatus == Status.Setting)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            ChannelIndex = 4;
            string direct = Close + Close + Close + Close + Open + Close + Close;
            PortData portData = new PortData { Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }
        public void ChangeMode()
        {
            if (ModeIndex >= ModeList.Length - 1)
            {
                ModeIndex = 0;
            }
            else
            {
                ModeIndex++;
            }

            SendChangeVarIcon(ModeList[ModeIndex]);
        }

        public void PrevPage()
        {
            if (CurrentStatus != Status.Init)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            string direct = "";
            if (ChannelIndex >= 0 && ChannelIndex < 4)
            {
                ChannelIndex = 6;
                direct = Close + Close + Close + Close + Close + Close + Open;
            }
            else if (ChannelIndex >= 4 && ChannelIndex < 6)
            {
                ChannelIndex = 0;
                direct = Open + Close + Close + Close + Close + Close + Close;
            }
            else
            {
                ChannelIndex = 4;
                direct = Close + Close + Close + Close + Open + Close + Close;
            }
            PortData portData = new PortData { Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }

        public void NextPage()
        {
            if (CurrentStatus != Status.Init)
            {
                return;
            }
            CurrentStatus = Status.Cutover;
            string direct = "";
            if (ChannelIndex >= 0 && ChannelIndex < 4)
            {
                ChannelIndex = 4;
                direct = Close + Close + Close + Close + Open + Close + Close;
            }
            else if (ChannelIndex >= 4 && ChannelIndex < 6)
            {
                ChannelIndex = 6;
                direct = Close + Close + Close + Close + Close + Close + Open;
            }
            else
            {
                ChannelIndex = 0;
                direct = Open + Close + Close + Close + Close + Close + Close;
            }
            PortData portData = new PortData { Address = ResistanceInputActiveModeAddress, Value = direct };
            SendChangeVarIcon(portData);
        }

        public void Cancel()
        {
            switch(CurrentStatus)
            {
                case Status.Setting:
                    Clear();
                    Num = OldNum;
                    SendOutputNum(OldNum);
                    break;
                case Status.Cutover:
                    // 关闭 当前 开启的通道
                    CloseChannel(ChannelIndex);
                    break;
            }
            CurrentStatus = Status.Init;
        }

        public async void Delete()
        {
            if (CurrentStatus == Status.Setting)
            {
                
                Clear();
                await Task.Delay(Delay);
                Num = Utils.RemoveLastCharacter(Num);
                SendOutputNum(Num);
            }
        }

        public void Set()
        {
            if (!Com.IsOpen)
            {
                MessageBox.Show("串口未连接成功，请重试");
                return;
            }
            CurrentStatus = Status.Setting;
        }

        public void Router(string PageAddress)
        {
            if (CurrentStatus != Status.Init)
            {
                return;
            }
            if (Com.IsOpen)
            {
                // 跳转
                SendData(PortHeader + "07" + WriteCode + "00845a0100" + PageAddress);
            }
        }


        // 仅用于测量通道切换时 使用  显示层面关闭所有测量
        public void CancelMeasurementResistance(Boolean onOff) {
            string direct = "";
            direct = onOff ? "000" + (ModeIndex + 3).ToString() : "000" + ModeIndex.ToString();
            string OnDirect = direct + Open + Open + Open;
            string OffDirect = direct + Close + Close + Close;

            PortData portData = new PortData { Address = ResistanceInputModeAddress, Value = onOff ? OnDirect : OffDirect };
            SendChangeVarIcon(portData);
        }

        public void Input()
        {
            string Val = "";
            if (InputStatus)
            {
                InputStatus = false;
                Val = Close;
            } else
            {
                InputStatus = true;
                Val = Open;
            }

            // TODO 发送 开启或关闭数据给硬件，成功开启会收到硬件返回值，根据返回数据判断是开启还是关闭
            PortData portData = new PortData { Address = InputOnOffIconAddress, Value = Val };
            SendChangeVarIcon(portData);

        }

        public void Output()
        {

            if (CurrentStatus != Status.Init)
            {
                return;
            }
            if (Num == "")
            {
                return;
            }

            double result = double.Parse(Num);
            Range range = RangeList[ChannelIndex];
            if (result > range.Max || result < range.Min)
            {
                Clear();
                Num = "";
                return;
            }
            
            OldNum = Num;
            string Val = "";
            if (OutputStatus)
            {
                OutputStatus = false;
                Val = Close;
            }
            else
            {
                OutputStatus = true;
                Val = Open;
                // TODO 在量程范围内，将数据保存OldNum，并发送数据给硬件
                // 将 Num 输出给硬件 
            }
            PortData portData = new PortData { Address = OutputOnOffIconAddress, Value = Val };
            SendChangeVarIcon(portData);

        }

        private void SendOutputNum(string Num)
        {
            string direct = PortHeader + (Num.Length + 5).ToString("X2") + WriteCode + OutputAddress + Utils.StringToHex(Num) + "FFFF";
            SendData(direct);
        }

        private void SendInputNum(string Num)
        {
            string direct = PortHeader + (Num.Length + 5).ToString("X2") + WriteCode + InputAddress + Utils.StringToHex(Num) + "FFFF";
            SendData(direct);
        }

        private void SendChangeVarIcon(PortData portData)
        {
            string v = portData.Value.Replace(" ", "");
            string direct = PortHeader + (v.Length/2 + 3).ToString("X2") + WriteCode + portData.Address + v;
            SendData(direct);
        }

        private void SetChannelOverviewAddress(PortData portData)
        {
            string direct = PortHeader + (portData.Value.Length + 3).ToString("X2") + WriteCode + portData.Address + Utils.StringToHex(portData.Value);
            SendData(direct);
        }

        private void SendData(string direct)
        {
            byte[] Command = Utils.StringToByteArray(direct);
            Com.Write(Command, 0, Command.Length);
        }

        private async void OpenChannel(int ChannelIndex)
        {
            string direct = "";
            string direct2 = "";
            string address = "";
            string address2 = "";
            if (ChannelIndex < 4)
            {
                address = ResistanceInputModeAddress;
                address2 = InputUnitAddress;
                direct2 = "000" + (ChannelIndex).ToString();
                for (int i = 0; i < 4; i++)
                {
                    if (ChannelIndex == 0)
                    {
                        direct += (i == ChannelIndex) ? "000" + (ModeIndex + 3).ToString() : Close;
                    } else
                    {
                        direct += (i == ChannelIndex) ? Open : (i == 0 ? "000" + ModeIndex.ToString() : Close);
                    }
                }
            } else if (ChannelIndex < 6 && ChannelIndex >= 4)
            {
                address = ResistanceOutModeAddress;
                address2 = OutputUnitAddress;
                direct2 = "000" + (ChannelIndex - 4).ToString();
                for (int i = 4; i < 6; i++)
                {
                    direct += (i == ChannelIndex) ? Open : Close;
                }
            }
            PortData portData = new PortData { Address = address, Value = direct };
            SendChangeVarIcon(portData);
            await Task.Delay(Delay);
            PortData portData2 = new PortData { Address = address2, Value = direct2 };
            SendChangeVarIcon(portData2);
        }

        private void CloseChannel(int ChannelIndex)
        {
            PortData portData = new PortData { Address = OverviewAddressList[ChannelIndex], Value = "0000" };
            SendChangeVarIcon(portData);
        }

        private void Clear() {
            if (CurrentStatus != Status.Setting)
            {
                return;
            }
            string direct = PortHeader + (6).ToString("X2") + WriteCode + OutputAddress + "20FFFF";
            SendData(direct);
        }

        public async void Test()
        {
            if (CurrentStatus != Status.Init)
            {
                return;
            }
            double result = Utils.GenerateRandomNumber();
            if (ChannelIndex < 4)
            {
                SendInputNum(result.ToString());
                await Task.Delay(Delay);
                SetChannelOverviewAddress(new PortData { Address = ChannelOverviewAddress[ChannelIndex], Value = result.ToString()});
            }
            else if (ChannelIndex >= 4 && ChannelIndex < 6)
            {
                SendOutputNum(result.ToString());
                await Task.Delay(Delay);
                SetChannelOverviewAddress(new PortData { Address = ChannelOverviewAddress[ChannelIndex], Value = result.ToString() });
            }
        }

        // 00 页 自检
        public void SelfPage00() {
            string direct = "0001 0001 0001 0001 0001 0001";
            PortData portData2 = new PortData { Address = SelfPage00PageAddress, Value = direct };
            SendChangeVarIcon(portData2);
        }

        // 01页 自检 修改指令

        public void SelfPage01()
        {
            string direct = "0001 0001";
            PortData portData2 = new PortData { Address = SelfTest01PageAddress, Value = direct };
            SendChangeVarIcon(portData2);
        }
    }

    class HardwareBoard
    {
        SerialPort Com;
        public HardwareBoard(SerialPort COM) {
            Com = COM;
            OpenCom();
        }

        private void OpenCom()
        {
            Com.PortName = "COM7";
            Com.BaudRate = Convert.ToInt32("115200");
            Com.ReceivedBytesThreshold = 1;
            Com.Open();
        }
        // 自检功能
        public void SelfTest()
        {
            SendData("00");
        }

        private void SendData(string direct)
        {
            byte[] Command = Utils.StringToByteArray(direct);
            Com.Write(Command, 0, Command.Length);
        }

        // 1.与硬件交互，检查电池电量，检查6通道是否可用
        // 2.检查电源，测量，输出，故障 四个指示灯的状态
        // 3.自检功能


    }
}
