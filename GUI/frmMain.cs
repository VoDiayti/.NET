using AppQLSV.DAL;
using AppQLSV.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLSV
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
            gridLopHoc.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            LoadDanhSachLopHoc();
        }
        void LoadDanhSachLopHoc()
        {
            //Coi như bài tập tại lớp
            AppQLSVDBContext db = new AppQLSVDBContext();
            var ls = db.Classrooms.OrderBy(e => e.Name).ToList();
            bdsLopHoc.DataSource = ls;
            gridLopHoc.DataSource = bdsLopHoc;
        }
        private void btnThemLop_Click(object sender, EventArgs e)
        {
            var f = new frmLopChiTiet();
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachLopHoc();
            }
        }

        private void btnXoaLop_Click(object sender, EventArgs e)
        {
            var lopDangChon = bdsLopHoc.Current as Classroom;
            if (lopDangChon != null)
            {
                var rs = MessageBox.Show(
                    "Bạn có thực sự muốn xóa không?",
                    "Chú ý",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    //Xóa lớp đang chọn
                    AppQLSVDBContext db = new AppQLSVDBContext();
                    var lop = db.Classrooms.Where(t => t.ID == lopDangChon.ID).FirstOrDefault();
                    if (lop != null)
                    {
                        db.Classrooms.Remove(lop);
                        db.SaveChanges();
                        LoadDanhSachLopHoc();
                    }
                }

            }
        }

        private void btnSuaLop_Click(object sender, EventArgs e)
        {
            var lopDangChon = bdsLopHoc.Current as Classroom;
            if (lopDangChon != null)
            {
                var f = new frmLopChiTiet(lopDangChon);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachLopHoc();
                }
            }
        }

        private void bdsLopHoc_CurrentChanged(object sender, EventArgs e)
        {
            var lopDangChon = bdsLopHoc.Current as Classroom;
            if (lopDangChon != null)
            {
                var db = new AppQLSVDBContext();
                var dsSV = db.Students.Where(t => t.IDClassroom == lopDangChon.ID).ToList();
                bdsSinhVien.DataSource = dsSV;
                dataGridView2.DataSource = bdsSinhVien;
            }
        }
        
        private void btnthemsinhvien_Click_1(object sender, EventArgs e)
        {
            var f = new ThemSinhVien();
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachLopHoc();
            }
        }

        private void btnsuasinhvien_Click_1(object sender, EventArgs e)
        {
            var sinhviendangchon = bdsSinhVien.Current as Student;
            if (sinhviendangchon != null)
            {
                var f = new ThemSinhVien(sinhviendangchon);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachLopHoc();
                }

            }
        }

        private void btnxoasinhvien_Click_1(object sender, EventArgs e)
        {
            var sinhviendangchon = bdsSinhVien.Current as Student;
            if (sinhviendangchon != null)
            {
                var rs = MessageBox.Show("Bạn có thật sự muốn xóa không ?", "Chú ý", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    //xóa lớp đang chọn
                    AppQLSVDBContext db = new AppQLSVDBContext();
                    var sv = db.Students.Where(t => t.ID == sinhviendangchon.ID).FirstOrDefault();

                    if (sv != null)
                    {
                        db.Students.Remove(sv);
                        db.SaveChanges();
                        LoadDanhSachLopHoc();
                    }

                }
            }
        }
    }
}
