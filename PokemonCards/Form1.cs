using System;
using System.Linq;
using System.Windows.Forms;

namespace PokemonCards
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        private int index;
        
        public Form1()
        {
            InitializeComponent();
            Instance = this;
            comboBox1.Items.AddRange(Pokemon.allPokemon.Select(entry => entry.Name).ToArray<object>());
            comboBox1.SelectedIndex = index;
            populateLabels();
        }

        public void populateLabels()
        {
            Data pokemon = Pokemon.allPokemon[index];
            pokemon.populate();
            this.label5.Text = pokemon.HealthPoints.ToString();
            this.label6.Text = pokemon.Strength.ToString();
            this.label7.Text = pokemon.SpecialPower;
            this.pictureBox1.ImageLocation = pokemon.ImageUrl;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.index = comboBox1.SelectedIndex;
            populateLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EditForm(index).Show();
        }
    }
}