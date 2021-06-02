using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        List<ScheduleItem> ScheduleItems;

        readonly string PathOfFileToWriteTo = "scheduleItems.txt";
        public Form1()
        {
            InitializeComponent();

            var enums = Enum.GetValues(typeof(ItemCategories));
            string[] enumNames = new string[enums.Length];
            for(int i = 0; i < enumNames.Length; i++)
            {
                enumNames[i] = enums.GetValue(i).ToString();
            }
            Category.DataSource = enumNames;
            
            int width = 0;
            foreach (var column in this.ScheduleGridView.Columns)
            {
                DataGridViewColumn col = (DataGridViewColumn)column;
                width += col.Width;
            }
            ScheduleGridView.Size = new Size(width + 55, ScheduleGridView.Height);

        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            ScheduleItems = new List<ScheduleItem>();
            for(int i = 0; i < ScheduleGridView.Rows.Count - 1; i++)
            {
                var row = ScheduleGridView.Rows[i];
                string name = (string)row.Cells[0].Value;
                ItemCategories category = (ItemCategories)Enum.Parse(typeof(ItemCategories), (string)row.Cells[1].Value);

                if (!DateTime.TryParse((string)row.Cells[2].Value, out DateTime startDate))
                {
                    row.Cells[2].Value = "";
                    MessageBox.Show($"The cell at row {i} in the Start Date column has the incorrect date format (example of proper format: 'June 2')");
                }
                if (!DateTime.TryParse((string)row.Cells[3].Value, out DateTime endDate))
                {
                    row.Cells[3].Value = "";
                    MessageBox.Show($"The cell at row {i} in the End Date column has the incorrect date format (example of proper format: 'June 2')");
                }
                
                int estimatedHours = int.Parse((string)row.Cells[4].Value);
                bool isFinished = ((DataGridViewCheckBoxCell)row.Cells[5]).Selected;
                
                var newItem = new ScheduleItem(name, category, startDate, endDate, estimatedHours, isFinished);
                ScheduleItems.Add(newItem);
            }

            string output = JsonConvert.SerializeObject(ScheduleItems);
            File.WriteAllText(PathOfFileToWriteTo, output);
        }
    }
}
