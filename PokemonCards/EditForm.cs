using System;
using System.Windows.Forms;

namespace PokemonCards
{
    public partial class EditForm : Form
    {
        private int index;
        private Data pokemon;
        
        public EditForm(int index)
        {
            InitializeComponent();
            this.index = index;
            this.pokemon = Pokemon.allPokemon[index];
            this.trackBar1.Maximum = 714;
            this.trackBar1.Minimum = 1;
            this.trackBar2.Maximum = 526;
            this.trackBar2.Minimum = 5;
            this.populate();
        }

        void populate()
        {
            this.label8.Text = this.pokemon.Name;
            this.label5.Text = this.pokemon.HealthPoints.ToString();
            this.trackBar1.Value = this.pokemon.HealthPoints;
            this.label6.Text = this.pokemon.Strength.ToString();
            this.trackBar2.Value = this.pokemon.Strength;
            this.label7.Text = this.pokemon.SpecialPower;
            this.textBox1.Text = this.pokemon.SpecialPower;
            this.pictureBox1.ImageLocation = this.pokemon.ImageUrl;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label5.Text = this.trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.label6.Text = this.trackBar2.Value.ToString();
        }

        private void textBox1_EnterPressed(object sender, EventArgs e)
        {
            if (((KeyEventArgs)e).KeyCode == Keys.Enter)
            {
                this.label7.Text = this.textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pokemon.HealthPoints = this.trackBar1.Value;
            this.pokemon.Strength = this.trackBar2.Value;
            this.pokemon.SpecialPower = this.label7.Text;
            Pokemon.allPokemon[index] = this.pokemon;
            Form1.Instance.populateLabels();
            this.Close();
        }
    }
}