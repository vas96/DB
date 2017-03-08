using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamBuilding
{
    #region Ліпше прочитати...
    /// <summary>
    /// По якійсь непонятній причині, entity не бачить проміжних таблиць 
    /// (тобто тих, завдяки яким утворюється звязок багато до багатьох)
    /// Хз чо так
    /// Для прикладу проміжна таблиця InProjects відносно юзера називається Projects2
    /// а таблиця CreatedProjects знову зі сторони юзвера носит назву Projects1
    /// В мене всьо
    /// </summary>
    #endregion

    public partial class UserProfile : Form
    {
        public TeamBuilding_Entities TB=new TeamBuilding_Entities();

        //ссилочка на форму з створенням проекта, шоби юзер
        //міг вернутися до неї, якшо захоче....
        public CreateProject User_Project;

        public ObservableCollection<Users> UsersList=new ObservableCollection<Users>();

        //Вибраний юзер
        public int SelectedUser=0;
        public UserProfile()
        {
            InitializeComponent();
            UsersList=new ObservableCollection<Users>(TB.Users);
            LoadUserData(SelectedUser);
        }

        public bool LoadUserData(int Selected)
        {
            try
            {
                ClearBoxes();

                Users Chosen_One = UsersList[Selected];
                //логін
                label1.Text = Chosen_One.Login;
                //імя і прізвище
                richTextBox1.Text = Chosen_One.Name + " " + Chosen_One.LastName;
                //заповнюєм список класів нашого юзверя
                foreach (var i in Chosen_One.Classes)
                {
                    listBox1.Items.Add(i.ClassName.ToString());
                }
                //а тут скіли закидуєм в список
                foreach (var i in Chosen_One.Skills)
                {
                    listBox2.Items.Add(i.SklName.ToString());
                }
                //куда наш юзер вписався
                foreach (var i in Chosen_One.Projects2)
                {
                    listBox3.Items.Add(i.PrjtName.ToString());
                }
                //контактіки
                //try-catch на випадок того, шо юзер не має контактів
                try
                {
                    Contacts User_Contacts = UsersList[Selected].Contacts;
                    {
                        if (User_Contacts.PublicMail != null)
                            textBox1.Text += "Mail::" + User_Contacts.PublicMail + "   ";
                        if (User_Contacts.Facebook != null)
                            textBox1.Text += "Facebook::" + User_Contacts.Facebook + "   ";
                        if (User_Contacts.VKId != null)
                            textBox1.Text += "VK::" + User_Contacts.VKId + "   ";
                        if (User_Contacts.Linkedin != null)
                            textBox1.Text += "Linkedin::" + User_Contacts.Linkedin + "   ";
                    }
                }
                catch (Exception){}

                //дата реєстрації
                label7.Text = Chosen_One.Registered.ToString();
                //к-сть створених проектів
                label8.Text = Chosen_One.Projects1.Count.ToString();
                //і на кінець - картіночку!
                pictureBox1.Image=new Bitmap(@"..\..\Pictures\"+Chosen_One.PicturePath);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public void ClearBoxes()
        {
            textBox1.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            richTextBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedUser > 0) SelectedUser--;
            LoadUserData(SelectedUser);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedUser + 1 < UsersList.Count) SelectedUser++;
            LoadUserData(SelectedUser);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User_Project= new CreateProject(this);
            this.Enabled = false;
            User_Project.Show();
        }
    }
}
