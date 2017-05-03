using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitDotNext.WithExistSyncContext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int before = Thread.CurrentThread.ManagedThreadId;

            // Захватили контекст (однопоточный), продолжение
            // выполняем в этом же контексте на том же потоке
            await Task.Delay(100);

            int after = Thread.CurrentThread.ManagedThreadId;

            MessageBox.Show($"Before: {before}, After: {after}");
        }
    }
}
