using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class PolygonRenderer : Form
    {
        #region 字段

        private int _mRendererMode; //渲染方式,0:简单渲染,1:唯一值渲染,2:分级渲染
        //简单渲染参数
        private Color _mSimpleRendererColor = Color.LightCoral; //符号颜色
        //唯一值渲染参数
        private int _mUniqueFieldIndex; //绑定字段索引
        //分级渲染参数
        private int _mClassBreaksFieldIndex; //绑定字段索引
        private int _mClassBreaksNum = 5; //分类数
        private Color _mClassBreaksRendererStartColor = Color.Olive; //符号起始颜色,面图层采用符号颜色进行分级表示
        private Color _mClassBreaksRendererEndColor = Color.Teal; //符号结束颜色,面图层采用符号颜色进行分级表示

        #endregion

        #region Constructors
        public PolygonRenderer(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            //填充字段下拉列表
            int fieldCount = layer.AttributeFields.Count;
            for (int i = 0; i < fieldCount; i++)
            {
                cboClassBreaksField.Items.Add(layer.AttributeFields.GetItem(i).Name);
            }
            cboClassBreaksField.SelectedIndex = 0;

            if (layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
            {
                MyMapObjects.moSimpleRenderer sRenderer = (MyMapObjects.moSimpleRenderer)layer.Renderer;
                MyMapObjects.moSimpleFillSymbol sSymbol = (MyMapObjects.moSimpleFillSymbol)sRenderer.Symbol;
                btnSimpleColor.BackColor = sSymbol.Color;
                _mSimpleRendererColor = sSymbol.Color;
                rbtnSimple.Checked = true;

            }
            else if (layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
            {
                MyMapObjects.moUniqueValueRenderer sRenderer = (MyMapObjects.moUniqueValueRenderer)layer.Renderer;
                MyMapObjects.moSimpleFillSymbol sSymbol = (MyMapObjects.moSimpleFillSymbol)sRenderer.GetSymbol(0);
                rbtnUniqueValue.Checked = true;

            }
            else if (layer.Renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
            {
                MyMapObjects.moClassBreaksRenderer sRenderer = (MyMapObjects.moClassBreaksRenderer)layer.Renderer;
                MyMapObjects.moSimpleFillSymbol sStartSymbol = (MyMapObjects.moSimpleFillSymbol)sRenderer.GetSymbol(0);
                MyMapObjects.moSimpleFillSymbol sEndSymbol = (MyMapObjects.moSimpleFillSymbol)sRenderer.GetSymbol(sRenderer.BreakCount - 1);
                cboClassBreaksField.SelectedIndex = layer.AttributeFields.FindField(sRenderer.Field);
                nudClassBreaksNum.Value = sRenderer.BreakCount;
                btnClassBreaksStartColor.BackColor = sStartSymbol.Color;
                _mClassBreaksRendererStartColor = sStartSymbol.Color;
                btnClassBreaksEndColor.BackColor = sEndSymbol.Color;
                _mClassBreaksRendererEndColor = sEndSymbol.Color;
                rbtnClassBreaks.Checked = true;
            }
            SetEnabled();
        }

        #endregion

        #region 窗体操作
        //设置选项是否可选
        private void SetEnabled()
        {
            if (rbtnSimple.Checked)
            {
                btnSimpleColor.Enabled = true;
                cboClassBreaksField.Enabled = false;
                nudClassBreaksNum.Enabled = false;
                btnClassBreaksStartColor.Enabled = false;
                btnClassBreaksEndColor.Enabled = false;
            }
            else if (rbtnUniqueValue.Checked)
            {
                btnSimpleColor.Enabled = false;
                cboClassBreaksField.Enabled = true;
                nudClassBreaksNum.Enabled = false;
                btnClassBreaksStartColor.Enabled = false;
                btnClassBreaksEndColor.Enabled = false;
            }
            else if (rbtnClassBreaks.Checked)
            {
                btnSimpleColor.Enabled = false;
                cboClassBreaksField.Enabled = true;
                nudClassBreaksNum.Enabled = true;
                btnClassBreaksStartColor.Enabled = true;
                btnClassBreaksEndColor.Enabled = true;

            }
        }


        //选择渲染方式
        private void GetRendererMode()
        {
            if (rbtnSimple.Checked)
            {
                _mRendererMode = 0;
            }
            else if (rbtnUniqueValue.Checked)
            {
                _mRendererMode = 1;
            }
            else if (rbtnClassBreaks.Checked)
            {
                _mRendererMode = 2;
            }
        }

        //选择简单渲染符号颜色
        private void btnSimpleColor_Click(object sender, EventArgs e)
        {
            DialogResult sColorDialogResult = cldPolygonRenderer.ShowDialog();
            if (sColorDialogResult == DialogResult.OK)
            {
                _mSimpleRendererColor = cldPolygonRenderer.Color;
                btnSimpleColor.BackColor = _mSimpleRendererColor;
            }
        }


        //选择分级渲染字段
        private void cboClassBreaksField_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mClassBreaksFieldIndex = cboClassBreaksField.SelectedIndex;
            _mUniqueFieldIndex = cboClassBreaksField.SelectedIndex;
        }

        //选择分级渲染分级数
        private void nudClassBreaksNum_ValueChanged(object sender, EventArgs e)
        {
            _mClassBreaksNum = (int)nudClassBreaksNum.Value;
        }

        //选择分级渲染符号起始颜色
        private void btnClassBreaksStartColor_Click(object sender, EventArgs e)
        {
            DialogResult sColorDialogResult = cldPolygonRenderer.ShowDialog();
            if (sColorDialogResult == DialogResult.OK)
            {
                _mClassBreaksRendererStartColor = cldPolygonRenderer.Color;
                btnClassBreaksStartColor.BackColor = _mClassBreaksRendererStartColor;
            }
        }

        //选择分级渲染符号终止颜色
        private void btnClassBreaksEndColor_Click(object sender, EventArgs e)
        {
            DialogResult sColorDialogResult = cldPolygonRenderer.ShowDialog();
            if (sColorDialogResult == DialogResult.OK)
            {
                _mClassBreaksRendererEndColor = cldPolygonRenderer.Color;
                btnClassBreaksEndColor.BackColor = _mClassBreaksRendererEndColor;
            }
        }

        //确认
        private void btnPolygonRendererConfirm_Click(object sender, EventArgs e)
        {
            MapCraftForm main = (MapCraftForm)Owner;
            GetRendererMode();
            main.Render.GetPolygonRenderer(_mRendererMode, _mSimpleRendererColor,
                _mUniqueFieldIndex, _mClassBreaksFieldIndex, _mClassBreaksNum,
                _mClassBreaksRendererStartColor, _mClassBreaksRendererEndColor);
            Close();
        }

        //取消
        private void btnPolygonRendererCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        private void rbtnSimple_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbtnUniqueValue_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }

        private void rbtnClassBreaks_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabled();
        }
    }
}
