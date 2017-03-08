using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 650;
            this.Height = 490;

   //         TeamBuildingEntities TB_BD = new TeamBuildingEntities();

 //           IQueryable<Project> All_Projects = TB_BD.Projects

              //                                  .Select(c => c);

            //All_Projects.ToList().Count();
            
      //      foreach (Project project in All_Projects)
      //    {
            Create();

          //  }
        }

        
        public void Create()
        {
            TeamBuildingEntities TB_BD = new TeamBuildingEntities();
            IQueryable<Project> All_Projects = TB_BD.Projects

                                                .Select(c => c);
            
            var len = All_Projects.ToList().Count();
            All_Projects.ToList();


            Panel back_basis = new Panel();
            back_basis.Name = "back_basis";
            back_basis.BackColor = Color.Green;
            back_basis.Width = 640;
            back_basis.Height = 480;
            back_basis.AutoScroll = true;
            int i = 0;

            foreach (Project p in All_Projects)
            { 
                Panel basis = new Panel();
                basis.Name = "basis" + i;
                basis.BackColor = Color.Yellow;
                basis.Location = new Point(10, i * 300);
                basis.Size = new Size(610, 250);

                Panel descriptionPanel = new Panel();
                descriptionPanel.Name = "description" + i;
                descriptionPanel.Left = basis.Left +210;
                descriptionPanel.Top = (basis.Top + 40);
                descriptionPanel.BackColor = Color.CadetBlue;
                descriptionPanel.Size = new Size(370, 150);

                i++;

                Label descriptionText = new Label();
                descriptionText.Text = p.PrjtDescription.ToString();
                descriptionText.Left = descriptionPanel.Left + 1;
                descriptionText.Top = (descriptionPanel.Top + 1);
                descriptionText.BringToFront();

                PictureBox header = new PictureBox();
                header.Left = basis.Left + 10;
                header.Top = (basis.Top + 10);
                header.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                header.TabIndex = 1;
                header.Width = 180;
                header.Height = 180;
                header.TabStop = false;
                header.BackColor = Color.Red;

                Label information = new Label();
                information.Text = p.PrjtCreated.ToString();
                information.Left = basis.Left + 240;
                information.Top = (basis.Top + 210);
                information.BringToFront();


                Label name = new Label();
                name.Text = p.PrjtName.ToString();
                name.Left = basis.Left + 240;
                name.Top = (basis.Top + 20);
                name.BringToFront();

                Button buttonLike = new Button();
                buttonLike.Text = "Like";
                buttonLike.Size = new System.Drawing.Size(180, 41);
                buttonLike.Left = (basis.Left + 10);
                buttonLike.Top = (basis.Top + 200);
                buttonLike.BackColor = Color.Blue;

                Button buttonNext = new Button();
                buttonNext.Text = "FullPost";
                buttonNext.Size = new System.Drawing.Size(180, 41);
                buttonNext.Left = (basis.Left + 400);
                buttonNext.Top = (basis.Top + 200);
                buttonNext.Click += new EventHandler(this.Button_Click_Next);
                buttonNext.BackColor = Color.BlueViolet;

                back_basis.Controls.Add(basis);

                basis.Controls.Add(descriptionPanel);
                descriptionPanel.Controls.Add(descriptionText);

                basis.Controls.Add(header);
                basis.Controls.Add(information);
                basis.Controls.Add(name);

                basis.Controls.Add(buttonLike);
                basis.Controls.Add(buttonNext);
                this.Controls.Add(back_basis);
            }

            //int len = 2;
            //for (int i = 0; i < len; i++)
        }

        private void Button_Click_Next(object sender, EventArgs e)
        {
            Form FullInformation = new Form();
            FullInformation.Width = 600;
            FullInformation.Height = 600;
            FullInformation.Show();

        }
    }
}

