namespace VisualPairCoding.WinForms
{
    public partial class NewTurnForm : Form
    {
        public NewTurnForm()
        {
            InitializeComponent();
        }

        public NewTurnForm(string message, bool transparent)
        {
            InitializeComponent();
            
            messageLabel.Text = message;
            messageLabel2.Text = message;

            TransparencyKey = Color.BlueViolet; // not transparent
            if (transparent)
                TransparencyKey = Color.White;
        }

        protected int AnimationTurns = 12;

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            var font = messageLabel.Font;
            float increment = AnimationTurns;

            messageLabel.Font = new Font(font.FontFamily, font.Size + increment, font.Style, font.Unit, font.GdiCharSet);
            messageLabel2.Font = new Font(font.FontFamily, font.Size + increment, font.Style, font.Unit, font.GdiCharSet);

            AnimationTurns -= 1;
            if (AnimationTurns <= 0)
            {
                animationTimer.Enabled = false;
                Close();
            }
        }

        private void NewTurnForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
