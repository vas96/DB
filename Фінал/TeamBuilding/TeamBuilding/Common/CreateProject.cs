using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamBuilding
{
    public partial class CreateProject : Form
    {
        public UserProfile UserProfile;
        public CreateProject(UserProfile Usr_Prf)
        {
            InitializeComponent();
            UserProfile = Usr_Prf;
            foreach (var i in UserProfile.TB.Classes)
            {
                checkedListBox1.Items.Add(i.ClassName.ToString());
            }
            foreach (var i in UserProfile.TB.Projects.ToList()[0].Skills)
            {
                checkedListBox2.Items.Add(i.SklName.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void CreateProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserProfile.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int For_id = UserProfile.TB.Projects.Count()-1;
            Projects New_Project = new Projects()
            {
                PrjtId = UserProfile.TB.Projects.ToList()[For_id].PrjtId+1,
                PrjtName = richTextBox1.Text,
                PrjtDescription = richTextBox2.Text,
                PrjtCreated = Convert.ToDateTime(DateTime.Today),
                PrjtImagePath = openFileDialog1.SafeFileName,
                PrjtCreatedBy = UserProfile.UsersList[UserProfile.SelectedUser].UsrId,
                PjrtLikeCounter = 0
            };
            UserProfile.UsersList[UserProfile.SelectedUser].Projects1.Add(New_Project);
            Image Img = new Bitmap(openFileDialog1.FileName);
            Img.Save(@"..\..\Pictures\" + openFileDialog1.SafeFileName);
            ObservableCollection<PrjtClasses> Class=new ObservableCollection<PrjtClasses>(UserProfile.TB.PrjtClasses);
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
                if (checkedListBox2.GetItemChecked(i))
                    New_Project.Skills.Add(UserProfile.TB.Skills.ToList()[i]);
            for (int i=0; i<checkedListBox1.Items.Count;i++)
                if (checkedListBox1.GetItemChecked(i))
                New_Project.PrjtClasses.Add(new PrjtClasses() { ClClassId = i,ClPrjtId = New_Project.PrjtId,
                    ClPeopleNeeded = "1", Classes = UserProfile.TB.Classes.ToList()[i], Projects = New_Project});
            UserProfile.TB.Projects.Add(New_Project);
            //тово гівно треба для дебага
            try
            {
                UserProfile.TB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show(validationError.PropertyName+
                                                validationError.ErrorMessage);
                    }
                }
            }
            UserProfile.LoadUserData(UserProfile.SelectedUser);
            Close();
        }
    }
}
