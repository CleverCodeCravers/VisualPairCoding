namespace VisualPairCoding.WinForms
{
    public partial class NewTurnForm : Form
    {
        private readonly bool _explicitChange;

        public NewTurnForm()
        {
            InitializeComponent();
        }

        public NewTurnForm(string message, bool transparent, bool explicitChange)
        {
            _explicitChange = explicitChange;
            InitializeComponent();
            
            messageLabel.Text = message;
            messageLabel2.Text = message;

            TransparencyKey = Color.BlueViolet; // not transparent
            if (transparent)
                TransparencyKey = Color.White;

            OkButton.Visible = _explicitChange;
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
                if (!_explicitChange)
                    Close();
            }
        }

        private void NewTurnForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_explicitChange)
                Close();
        }
    }
}
