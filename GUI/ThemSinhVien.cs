using AppQLSV.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLSV.GUI
{
    public partial class ThemSinhVien : Form
    {
        Student sinhvien;
        public ThemSinhVien()
        {
            //thêm mới
            InitializeComponent();
            loadcombobox();
            this.Text = "Thêm mới sinh viên";
        }


        public ThemSinhVien(Student sinhvien)
        {
            //chỉnh sửa
            InitializeComponent();
            loadcombobox();
            this.sinhvien = sinhvien;
            this.Text = "Chỉnh sửa sinh viên";
            txtfirstnam.Text = this.sinhvien.FirstName;
            txtlastname.Text = this.sinhvien.LastName;
            this.sinhvien.DateOfBirth = txtbirthday.Value;
            txtpod.Text = this.sinhvien.PlaceOfBirth;
            cbMalop.Text = this.sinhvien.IDClassroom;
            var gender = 0;
            if (rdbMale.Checked == true)
            {
                gender = 0;
            }
            else if (rdbFeMale.Checked == true)
            {
                gender = 1;
            }
            else if (rdbOthersexual.Checked == true)
            {
                gender = 3;
            }
            this.sinhvien.Gender = gender;


        }

        void loadcombobox()
        {
            var db = new AppQLSVDBContext();
            var cbbox = db.Classrooms.OrderBy(e => e.ID).ToList();
            for (int i = 0; i < cbbox.Count; i++)
            {
                cbMalop.Items.Add(cbbox.ElementAt(i).ID);
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            {
                var firstname = txtfirstnam.Text;
                var lastname = txtlastname.Text;
                var dob = txtbirthday.Value;
                var pod = txtpod.Text;
                var gender = 0;
                var idclassrom = cbMalop.Text;
                if (rdbMale.Checked == true)
                {
                    gender = 0;
                }
                else if (rdbFeMale.Checked == true)
                {
                    gender = 1;
                }
                else if (rdbOthersexual.Checked == true)
                {
                    gender = 3;
                }

                if (sinhvien == null)
                {

                    System.Random rd = new Random();
                    var sv = new Student
                    {
                        ID = "T"+rd.Next(1,9999), //Guid.NewGuid().ToString(),
                        FirstName = firstname,
                        LastName = lastname,
                        DateOfBirth = dob,
                        PlaceOfBirth = pod,
                        Gender = gender,
                        IDClassroom = idclassrom
                    };
                    var db = new AppQLSVDBContext();
                    db.Students.Add(sv);
                    db.SaveChanges();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    var db = new AppQLSVDBContext();
                    var newsv = db.Students.Where(t => t.ID == sinhvien.ID).FirstOrDefault();
                    newsv.FirstName = firstname;
                    newsv.LastName = lastname;
                    newsv.DateOfBirth = dob;
                    newsv.PlaceOfBirth = pod;
                    newsv.Gender = gender;
                    newsv.IDClassroom = idclassrom;
                    db.SaveChanges();
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
