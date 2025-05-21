using com.calitha.goldparser;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        MyParser P;
        public Form1()
        {
            InitializeComponent();
            P = new MyParser("TablesOfGrammar.cgt", listBox1, listBox2);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            P.Parse(richTextBox1.Text);
        }
    }
}
