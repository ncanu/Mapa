using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Mapa
{
    public partial class RutaMapa : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        int filaSeleccionada = 0;
        double LatInicial = 20.9688132813906;
        double LngInicial = -89.6250915527344;


        public RutaMapa()
        {
            InitializeComponent();
        }

        private void RutaMapa_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Dirección", typeof(string)) );
            dt.Columns.Add(new DataColumn("Latitud", typeof(double)));
            dt.Columns.Add(new DataColumn("Longitud", typeof(double)));

            dt.Rows.Add( "Ubicación 1", LatInicial, LngInicial );
            dataGridView1.DataSource = dt;


            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;


            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial),GMarkerGoogleType.red);
            markerOverlay.Markers.Add(marker);


            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud: {0}", LatInicial, LngInicial);

            gMapControl1.Overlays.Add(markerOverlay);

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaSeleccionada = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[filaSeleccionada].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[filaSeleccionada].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[filaSeleccionada].Cells[2].Value.ToString();

            marker.Position = new PointLatLng(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3));
            gMapControl1.Position = marker.Position;

        }
    }
}
