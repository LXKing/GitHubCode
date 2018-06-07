using IC.BLL;
using IC.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IC.ChargeSystemMain
{
    public partial class Form1 : Form
    {
        Action<string ,int> ActionUpdateProcess;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_NewReset_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) => {
                
            };
            worker.DoWork += ( sender1, e1 )=>
            {
                var res = cmd.ResetNewCard();
                if(res.Success)
                    UpdateProcess("初始化新卡成功!", 0);
                
                else
                    UpdateProcess(res.Message, 0);
            };
            
            
            DialogResult dr = MessageBox.Show("是否初始化为新卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if(dr==DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
            
            //var eleMeterBll = new BLL_EleMeterCharge();
            //eleMeterBll.NewCard("00000001");
        }

        void cmd_OperationChange(object arg1, OperatingChangeEventArgs arg2)
        {
            UpdateProcess(arg2.OpeationMsg, arg2.OpeatiePercentage);
        }

        public void UpdateProcess(string text,int percent)
        {
            if(progressBar1.InvokeRequired)
            {
                ActionUpdateProcess  = UpdateProcess;
                progressBar1.Invoke(ActionUpdateProcess,new object[]{text, percent});
            }
            else
            {
                progressBar1.Text = text;
                progressBar1.Value = percent;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {

            };
            worker.DoWork += (sender1, e1) =>
            {
                var res = cmd.PublishNewCard(SystemParmetersConfig.AreaCode,Convert.ToInt32(numericUpDown1.Value),SystemParmetersConfig.SystemParmValue,SystemParmetersConfig.CheckCodeValue);
                if (res.Success)
                    UpdateProcess("新开卡成功!", 0);

                else
                    UpdateProcess(res.Message, 0);
            };


            DialogResult dr = MessageBox.Show("是否开新卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }

        private void btn_ChargeEle_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {

            };
            worker.DoWork += (sender1, e1) =>
            {
                var res = cmd.ChargeEle(Convert.ToInt32(num_Count.Value), SystemParmetersConfig.SystemParmValue,Convert.ToInt32(num_EleBuy.Value),SystemParmetersConfig.LimitPowerValue,SystemParmetersConfig.StopEleValue,SystemParmetersConfig.AlarmEleValue, SystemParmetersConfig.CheckCodeValue);
                if (res.Success)
                    UpdateProcess("买电成功!", 0);

                else
                    UpdateProcess(res.Message, 0);
            };


            DialogResult dr = MessageBox.Show("是否开买电?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            progressBar1.Value=0;
            progressBar1.Maximum = 16777215;
            string text = string.Empty;
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {
                var result = (CardOperateResult)e2.Result;
                if(result.Success)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.AppendFormat("内码:{0}\n\r", ((EleCardInfo)(result.Data)).InnerNOHexString);
                    msg.AppendFormat("区号:{0}\n\r", ((EleCardInfo)(result.Data)).AreaCodeHexString);
                    msg.AppendFormat("卡号:{0}\n\r", ((EleCardInfo)(result.Data)).CardCodeHexString);
                    msg.AppendFormat("表中次数:{0}\n\r", ((EleCardInfo)(result.Data)).ChargeCountsInMeterHexString);
                    msg.AppendFormat("卡中次数:{0}\n\r", ((EleCardInfo)(result.Data)).ChargeCountsInCardHexString);
                    msg.AppendFormat("卡中购电量:{0}\n\r", ((EleCardInfo)(result.Data)).ChargeEleInCardHexString);
                    msg.AppendFormat("总电量:{0}\n\r", ((EleCardInfo)(result.Data)).SumEleInMeterHexString);
                    msg.AppendFormat("欠电量:{0}\n\r", ((EleCardInfo)(result.Data)).TuitionEleHexString);
                    msg.AppendFormat("稽查码:{0}\n\r", ((EleCardInfo)(result.Data)).CheckCodeValueHexString);
                    msg.AppendFormat("是否窃电:{0}\n\r", ((EleCardInfo)(result.Data)).StealEleFalg);
                    msg.AppendFormat("剩余电量:{0}\n\r", ((EleCardInfo)(result.Data)).SurplusEleHexString);
                    MessageBox.Show(msg.ToString());
                }
                
            };
            worker.DoWork += (sender1, e1) =>
            {
                e1.Result =  cmd.ReadEleCardInfo(SystemParmetersConfig.AreaCode);
            };


            DialogResult dr = MessageBox.Show("是否开始读取电卡数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 单项清零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 16777215;
            string text = string.Empty;
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {
                var result = (CardOperateResult)e2.Result;
                if (result.Success)
                {
                    MessageBox.Show(result.Message);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            };
            worker.DoWork += (sender1, e1) =>
            {
                e1.Result = cmd.MakeSingleCleanCard();
            };


            DialogResult dr = MessageBox.Show("是否开始发型清零卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 撤销费用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 16777215;
            string text = string.Empty;
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {
                var result = (CardOperateResult)e2.Result;
                if (result.Success)
                {
                    MessageBox.Show(result.Message);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            };
            worker.DoWork += (sender1, e1) =>
            {
                e1.Result = cmd.RevokedEle(SystemParmetersConfig.AreaCode,1);
            };


            DialogResult dr = MessageBox.Show("是否进行撤销上次购电?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                return new CardOperateResult(){Data =  new UserCardCommand().ReadData((int)beginAddress.Value,(int)length.Value),Success=true};
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                if (result.Success)
                {
                    var data = (List<byte>)(result.Data);
                    MessageBox.Show(data.ByteToHexString(" "));

                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否进行读取数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }
        /// <summary>
        /// 读取水卡数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () => {
                return new UserCardCommand().ReadWaterCardInfo(SystemParmetersConfig.AreaCode);
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) => {
                var result = e1.Result as CardOperateResult;
                if(result.Success)
                {
                    var waterData = (WaterCardInfo)(result.Data);
                    var water0=((WaterCardInfo)(result.Data)).WaterMetersCollection.FirstOrDefault();
                    if(water0!=null)
                    {
                        StringBuilder msg = new StringBuilder();
                        msg.AppendFormat("内码:{0}\n\r", waterData.InnerNOHexString);
                        msg.AppendFormat("区号:{0}\n\r", waterData.AreaCodeHexString);
                        msg.AppendFormat("卡号:{0}\n\r", waterData.CardCodeHexString);
                        msg.AppendFormat("卡中次数:{0}\n\r", water0.ChargeCountInCard);
                        msg.AppendFormat("卡中购水量:{0}\n\r", water0.ChargeWater);
                        msg.AppendFormat("累计使用量:{0}\n\r", water0.SumUsedWater);
                        msg.AppendFormat("剩余量:{0}\n\r", water0.SurplusWater);
                        MessageBox.Show(msg.ToString());
                    }
                    
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            };

            Func<DialogResult> beforeRunAsk = () => {
                return MessageBox.Show("是否进行读取水卡数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }
        /// <summary>
        /// 公共方法
        /// </summary>
        /// <param name="runAction"></param>
        /// <param name="successAction"></param>
        /// <param name="beforeRunAsk"></param>
        private void RunWorker(Func<CardOperateResult> runAction,Action<RunWorkerCompletedEventArgs> successAction,Func<DialogResult> beforeRunAsk)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            string text = string.Empty;
            worker = new BackgroundWorker();
            IC.Command.UserCardCommand cmd = new Command.UserCardCommand();
            cmd.OperationChange += cmd_OperationChange;

            worker.RunWorkerCompleted += (sender2, e2) =>
            {
                successAction.Invoke(e2);
            };
            worker.DoWork += (sender1, e1) =>
            {
                e1.Result = runAction.Invoke();
            };


            DialogResult dr = beforeRunAsk.Invoke();//MessageBox.Show("是否进行撤销上次购电?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 水表清零卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                return new UserCardCommand().MakeWaterCleanCard();
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否发行水表清零卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }

        /// <summary>
        /// 水表分类卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                return new UserCardCommand().MakeWaterClassifiedCard(Convert.ToInt32(numMeterNO.Value));
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否发行水表分类卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }
        /// <summary>
        /// 发行水表时钟卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                return new UserCardCommand().MakeWaterClockCard(System.DateTime.Now);
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否发行水表时钟卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }
        /// <summary>
        /// 水表磁解除卡(管理卡)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                return new UserCardCommand().MakeWaterMagneticLiftingCard();
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否发行水表磁解除卡?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                var waterCardList=new List<WaterMeterInfoEntity>();
                if(checkBox1.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() {  ChargeCountInCard = (int)water1Count.Value, MeterNO= 1, ChargeWater=Convert.ToDouble(water1Charge.Value)});
                }
                if (checkBox2.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 2, ChargeWater = Convert.ToDouble(water2Charge.Value) });
                }
                if (checkBox3.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 3, ChargeWater = Convert.ToDouble(water3Charge.Value) });
                }
                if (checkBox4.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 4, ChargeWater = Convert.ToDouble(water4Charge.Value) });
                }
                if (checkBox5.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 5, ChargeWater = Convert.ToDouble(water5Charge.Value) });
                }
                if (checkBox6.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 6, ChargeWater = Convert.ToDouble(water6Charge.Value) });
                }
                if (checkBox7.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 7, ChargeWater = Convert.ToDouble(water7Charge.Value) });
                }
                return new UserCardCommand().ChargeWater(SystemParmetersConfig.AreaCode,waterCardList);
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否进行购水充值?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Func<CardOperateResult> runAction = () =>
            {
                var waterCardList = new List<WaterMeterInfoEntity>();
                if (checkBox1.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water1Count.Value, MeterNO = 1, ChargeWater = Convert.ToDouble(water1Charge.Value) });
                }
                if (checkBox2.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 2, ChargeWater = Convert.ToDouble(water2Charge.Value) });
                }
                if (checkBox3.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 3, ChargeWater = Convert.ToDouble(water3Charge.Value) });
                }
                if (checkBox4.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 4, ChargeWater = Convert.ToDouble(water4Charge.Value) });
                }
                if (checkBox5.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 5, ChargeWater = Convert.ToDouble(water5Charge.Value) });
                }
                if (checkBox6.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 6, ChargeWater = Convert.ToDouble(water6Charge.Value) });
                }
                if (checkBox7.Checked)
                {
                    waterCardList.Add(new WaterMeterInfoEntity() { ChargeCountInCard = (int)water2Count.Value, MeterNO = 7, ChargeWater = Convert.ToDouble(water7Charge.Value) });
                }
                return new UserCardCommand().RevokedWater(SystemParmetersConfig.AreaCode,1,waterCardList);
            };
            Action<RunWorkerCompletedEventArgs> successAction = (e1) =>
            {
                var result = e1.Result as CardOperateResult;
                MessageBox.Show(result.Message);
            };

            Func<DialogResult> beforeRunAsk = () =>
            {
                return MessageBox.Show("是否撤销购水充值?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            };

            RunWorker(runAction, successAction, beforeRunAsk);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes==MessageBox.Show("是否验证密码算法?","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
            {
                try
                {
                    var cardResult = new IC.Command.UserCardCommand().JudgeCard();
                    if (cardResult == JudgeCardResult.HasBadCard)
                    {
                        MessageBox.Show("验证算法无效，请立即停止使用!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button11.Enabled = false;
                    }
                    else if (cardResult == JudgeCardResult.HasOKNewCard)
                    {
                        MessageBox.Show("这是一张新卡!", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (cardResult == JudgeCardResult.HasOKOldCard)
                    {
                        MessageBox.Show("验证算法有效!", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
