using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Project1Main
{
    /// <summary>
    /// This class handles all of the user interaction.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ShapeCollection shapes = new ShapeCollection();

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Shape shape in shapes)
            {
                if (e.ClipRectangle.IntersectsWith(shape.GetLargesetPossibleRegion()))
                {
                    shape.Render(e.Graphics);
                }
            }
        }

        private City currentSelectedCity;

        private void ClearSelectedShape()
        {
            if (currentSelectedCity != null)
            {
                currentSelectedCity.Selected = false;
                panel.Invalidate(currentSelectedCity.GetLargesetPossibleRegion());
            }
            currentSelectedCity = null;
        }

        private bool isDragging;
        private int clickOffsetX, clickOffsetY;

        // keeps track of progress in getting line drawn.
        private bool firstShapeSelected;
        City tailCity, headCity;
        Point newCityLocation;

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (newCityToolEnabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    newCityLocation = new Point(e.X, e.Y);

                    CityNameDialog cityNameDialog = new CityNameDialog(astar.Vertices);
                    if (cityNameDialog.ShowDialog() == DialogResult.OK)
                    {
                        AddCityToView(cityNameDialog.CityName, newCityLocation);
                        Vertex newVertex = new Vertex(cityNameDialog.CityName,
                            convertScreenXcoordToModelXCoord(newCityLocation.X),
                            convertScreenYcoordToModelYCoord(newCityLocation.Y),
                            astar.Vertices.Count);
                        astar.Vertices.Add(newVertex);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    BringCitiesToFront();
                    ClearSelectedShape();

                    currentSelectedCity = shapes.HitTest(new Point(e.X, e.Y)) as City;

                    if (currentSelectedCity != null)
                    {
                        contextMenu_EditCity.Show(panel, new Point(e.X, e.Y));
                    }
                }
            }
            else if (oneWayPathToolEnabled || twoWayPathToolEnabled)
            {
                // move cities to front so that they get priority for selection
                BringCitiesToFront();
                ClearSelectedShape();

                currentSelectedCity = shapes.HitTest(new Point(e.X, e.Y)) as City;

                if (currentSelectedCity != null)
                {   // the user clicked on a city
                    if (e.Button == MouseButtons.Right)
                    {
                        contextMenu_EditCity.Show(panel, new Point(e.X, e.Y));
                        if (tailCity != null)
                        {
                            tailCity.Selected = true;
                        }
                    }
                    else if (e.Button == MouseButtons.Left) { 

                        if (!firstShapeSelected)
                        {
                            firstShapeSelected = true;
                            tailCity = currentSelectedCity as City;
                            currentSelectedCity.Selected = true;
                            panel.Invalidate(currentSelectedCity.GetLargesetPossibleRegion());
                        }
                        else
                        {   // second shape is selected we can now create line and draw it.

                            // make sure they didn't click the same city twice
                            if (tailCity != currentSelectedCity)
                            {
                                // Check to make sure there isn't already a line connecting these two
                                // cities before adding a new line.
                                if (goodLine(tailCity, currentSelectedCity as City))
                                {
                                    headCity = currentSelectedCity as City;
                                    if (oneWayPathToolEnabled)
                                    {
                                        drawPath(tailCity, headCity);
                                        AddConnectionToModel(tailCity.Name, headCity.Name);

                                    }
                                    else if (twoWayPathToolEnabled)
                                    {
                                        drawPath(tailCity, headCity);
                                        AddConnectionToModel(tailCity.Name, headCity.Name);
                                        drawPath(headCity, tailCity);
                                        AddConnectionToModel(headCity.Name, tailCity.Name);
                                    }
                                    panel.Invalidate(tailCity.GetLargesetPossibleRegion());
                                    tailCity.Selected = false;
                                }
                                else
                                {
                                    MessageBox.Show("There already exists a path between these two cities");
                                    tailCity.Selected = true;
                                    panel.Invalidate(tailCity.GetLargesetPossibleRegion());
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (firstShapeSelected)
                    {
                        tailCity.Selected = true;
                    }
                }
            }
            else // no tools enabled
            {
                BringCitiesToFront();
                ClearSelectedShape();

                currentSelectedCity = shapes.HitTest(new Point(e.X, e.Y)) as City;

                if (currentSelectedCity != null)
                {
                    currentSelectedCity.Selected = true;
                    panel.Invalidate(currentSelectedCity.GetLargesetPossibleRegion());

                    if (e.Button == MouseButtons.Right)
                    {
                        contextMenu_EditCity.Show(panel, new Point(e.X, e.Y));
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        clickOffsetX = e.X - currentSelectedCity.Location.X;
                        clickOffsetY = e.Y - currentSelectedCity.Location.Y;
                        isDragging = true;
                    }
                }
            }
        }

        private void AddConnectionToModel(string tailCity, string headCity)
        {
            Vertex tailVertex = astar.Vertices.Find(v => v.Name == tailCity);
            Vertex headVertex = astar.Vertices.Find(v => v.Name == headCity);
            tailVertex.AdjList.Add(headVertex.ID);
        }

        private void AddCityToView(string name, Point newCityLocation)
        {
            City newCity = new City(name);
            newCity.Size = new Size(citySize, citySize);
            newCity.Color = Color.Brown;
            newCityLocation.X -= citySize / 2;
            newCityLocation.Y -= citySize / 2;
            newCity.Location = newCityLocation;
            newCity.PenThickness = 2;
            shapes.Add(newCity);
            panel.Invalidate();
        }

        private void BringCitiesToFront()
        {
            foreach (Shape shape in shapes)
            {
                City city = shape as City;
                if (city != null)
                {
                    shapes.BringShapeToFront(city);
                }
            }
        }

        private void drawPath(City tailCity, City headCity)
        {
            Arrow arrow = AddPath(tailCity, headCity);
            firstShapeSelected = false;
            panel.Invalidate(arrow.GetLargesetPossibleRegion());
        }

        private Arrow AddPath(City tailCity, City headCity)
        {
            Arrow arrow = new Arrow();
            arrow.Tail = tailCity;
            arrow.Head = headCity;
            arrow.Color = Color.Black;
            arrow.PenThickness = 1;
            tailCity.ConntectedLines.Add(arrow);
            headCity.ConntectedLines.Add(arrow);
            shapes.Add(arrow);
            return arrow;
        }

        private bool goodLine(City tailCity, City currentSelectedCity)
        {
            bool lineOK = true;
            foreach (Shape shape in shapes)
            {
                Arrow arrow = shape as Arrow;
                if (arrow != null)
                {
                    if (tailCity == arrow.Tail && currentSelectedCity == arrow.Head)
                    {
                        lineOK = false;
                    }
                }
            }
            return lineOK;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Rectangle oldPosition = currentSelectedCity.GetLargesetPossibleRegion();
                currentSelectedCity.Location = new Point(e.X - clickOffsetX, e.Y - clickOffsetY);
                UpdateModelLocation(currentSelectedCity);

                Rectangle newPosition = currentSelectedCity.GetLargesetPossibleRegion();

                Rectangle aggregate = new Rectangle();
                foreach (Arrow line in currentSelectedCity.ConntectedLines)
                {
                    if (aggregate.Width == 0 && aggregate.Height == 0)
                    {
                        aggregate = line.GetLargesetPossibleRegion();
                    }
                    else
                    {
                        aggregate = Rectangle.Union(aggregate, line.GetLargesetPossibleRegion());
                    }
                   
                }

                Rectangle r = Rectangle.Union(oldPosition, newPosition);
                if (aggregate.Width == 0 && aggregate.Height == 0)
                {
                    panel.Invalidate(r);
                } 
                else 
                {
                    panel.Invalidate(Rectangle.Union(aggregate, r));
                }
            }
        }

        private void UpdateModelLocation(City currentSelectedCity)
        {
            Vertex vertex = astar.Vertices.Find(v => v.Name == currentSelectedCity.Name);
            vertex.X = convertScreenXcoordToModelXCoord(currentSelectedCity.Location.X);
            vertex.Y = convertScreenYcoordToModelYCoord(currentSelectedCity.Location.Y);
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void removeCityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentSelectedCity == startCity) startCity = null;
            if (currentSelectedCity == endCity) endCity = null;
            RemoveCityFromView();
            Utilties.deleteVertex(currentSelectedCity.Name);
            ClearSelectedShape();
            panel.Invalidate();
        }

        private void RemoveCityFromView()
        {
            shapes.Remove(currentSelectedCity);

            Rectangle aggregate = new Rectangle();
            foreach (Arrow line in currentSelectedCity.ConntectedLines)
            {
                if (aggregate.Width == 0 && aggregate.Height == 0)
                {
                    aggregate = line.GetLargesetPossibleRegion();
                }
                else
                {
                    aggregate = Rectangle.Union(aggregate, line.GetLargesetPossibleRegion());
                }

                shapes.Remove(line);
                foreach (Shape shape in shapes)
                {
                    City city = shape as City;
                    if (city != null)
                    {
                        for (int i = 0; i < city.ConntectedLines.Count; i++)
                        {
                            Arrow a = city.ConntectedLines[i] as Arrow;
                            if (line == a)
                            {
                                city.ConntectedLines.Remove(a);
                            }
                        }
                    }
                }
            }

            Rectangle r = currentSelectedCity.GetLargesetPossibleRegion();
            if (aggregate.Width == 0 && aggregate.Height == 0)
            {
                panel.Invalidate(r);
            }
            else
            {
                panel.Invalidate(Rectangle.Union(aggregate, r));
            } 
        }
        private const int citySize = 15;

        bool newCityToolEnabled;
        bool oneWayPathToolEnabled;
        bool twoWayPathToolEnabled;

        private void checkBox_NewCity_CheckedChanged(object sender, EventArgs e)
        {
            if (!newCityToolEnabled && !oneWayPathToolEnabled && !twoWayPathToolEnabled)
            {
                newCityToolEnabled = true;
            }
            else
            {
                if (!newCityToolEnabled)
                {
                    newCityToolEnabled = true;
                    if (oneWayPathToolEnabled)
                    {
                        checkBox_1WayPath.Checked = false;
                        tailCity = null;
                        firstShapeSelected = false;
                        ClearSelectedShape();
                    }
                    else if (twoWayPathToolEnabled)
                    {
                        checkBox_2WayPath.Checked = false;
                        tailCity = null;
                        firstShapeSelected = false;
                        ClearSelectedShape();
                    }
                }
                else
                {
                    newCityToolEnabled = false;
                }
            }
        }

        private void checkBox_1WayPath_CheckedChanged(object sender, EventArgs e)
        {
            if (!newCityToolEnabled && !oneWayPathToolEnabled && !twoWayPathToolEnabled)
            {
                oneWayPathToolEnabled = true;
            }
            else
            {
                if (!oneWayPathToolEnabled)
                {
                    oneWayPathToolEnabled = true;
                    if (newCityToolEnabled)
                    {
                        checkBox_NewCity.Checked = false;
                    }
                    else if (twoWayPathToolEnabled)
                    {
                        checkBox_2WayPath.Checked = false;
                        
                    }
                }
                else
                {
                    oneWayPathToolEnabled = false;
                    tailCity = null;
                    firstShapeSelected = false;
                    ClearSelectedShape();
                }
            }
        }

        private void checkBox_2WayPath_CheckedChanged(object sender, EventArgs e)
        {
            if (!newCityToolEnabled && !oneWayPathToolEnabled && !twoWayPathToolEnabled)
            {
                twoWayPathToolEnabled = true;
            }
            else
            {
                if (!twoWayPathToolEnabled)
                {
                    twoWayPathToolEnabled = true;
                    if (newCityToolEnabled)
                    {
                        checkBox_NewCity.Checked = false;
                    }
                    else if (oneWayPathToolEnabled)
                    {
                        checkBox_1WayPath.Checked = false;
                    }
                }
                else
                {
                    twoWayPathToolEnabled = false;
                    tailCity = null;
                    firstShapeSelected = false;
                    ClearSelectedShape();
                }
            }
            ClearSelectedShape();
        }

        AStar astar;

        bool locationFileSelected;
        bool connectionFileSelected;

        private void button_LocationFile_Click(object sender, EventArgs e)
        {
            if (connectionFileSelected)
            {
                GetFile(textBox_LocationFile);
                locationFileSelected = true;
                try
                {
                    RenderMap();
                }
                catch
                {
                    MessageBox.Show("Incorrect file format.");
                    return;
                }
                EnableUI();
            }
            else
            {
                GetFile(textBox_LocationFile);
                locationFileSelected = true;
            }
        }

        private void EnableUI()
        {
            groupBox_Tools.Enabled = true;
            groupBox_Heuristic.Enabled = true;
            button_GO.Enabled = true;
            button_Step.Enabled = true;
            button_FinalPath.Enabled = true;
        }

        private void button_ConnectionsFile_Click(object sender, EventArgs e)
        {
            if (locationFileSelected)
            {
                GetFile(textBox_ConnectionsFile);
                connectionFileSelected = true;
                try 
                {
                    RenderMap();
                }
                catch
                {
                    MessageBox.Show("Incorrect file format." );
                    return;
                }
                EnableUI();
            }
            else
            {
                GetFile(textBox_ConnectionsFile);
                connectionFileSelected = true;
            }
        }

        private void GetFile(TextBox textBox)
        {
            textBox.Text = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.Cancel)
            {
                textBox.Text = dlg.FileName;
            }
        }

        private void RenderMap()
        {
            astar = new AStar(textBox_LocationFile.Text, textBox_ConnectionsFile.Text);
            FindExtremeCoordinates();
            AddCities();
            AddPaths();
            panel.Invalidate();
        }

        const int padding = 70;

        int maxXCoord, maxYCoord, minXCoord, minYCoord;
        private void FindExtremeCoordinates()
        {
            maxXCoord = int.MinValue;
            maxYCoord = int.MinValue;
            minXCoord = int.MaxValue;
            minYCoord = int.MaxValue;

            foreach (Vertex v in astar.Vertices)
            {
                if (v.X > maxXCoord)
                {
                    maxXCoord = v.X;
                }
                if (v.Y > maxYCoord)
                {
                    maxYCoord = v.Y;
                }
                if (v.X < minXCoord)
                {
                    minXCoord = v.X;
                }
                if (v.Y < minYCoord)
                {
                    minYCoord = v.Y;
                }
            }
        }

        private void AddCities()
        {
            List<Vertex> vertices = astar.Vertices;
            for (int i = 1; i < vertices.Count; i++)
            {
                Point point = new Point(convertModelXCoordToScreenXCoord(vertices[i].X), 
                    convertModelYCoordToScreenYCoord(vertices[i].Y));
                AddCityToView(vertices[i].Name, point);
            }
        }

        private int convertModelXCoordToScreenXCoord(int fileXCoordinate)
        {
            double fileRange = maxXCoord - minXCoord;
            double screenRange = (panel.Size.Width - padding) - padding;
            return (int)(((fileXCoordinate - minXCoord) * screenRange) / fileRange) + padding;
        }

        private int convertModelYCoordToScreenYCoord(int fileYCoordinate)
        {
            double fileRange = maxYCoord - minYCoord;
            double screenRange = (panel.Size.Height - padding) - padding;
            return (int)(((fileYCoordinate - minYCoord) * screenRange) / fileRange) + padding;
        }

        private int convertScreenXcoordToModelXCoord(int screenXCoordinate)
        {
            double screenRange = (panel.Size.Width - padding) - padding;
            double fileRange = maxXCoord - minXCoord;
            return (int)(((screenXCoordinate - padding) * fileRange) / screenRange) + minXCoord;
        }

        private int convertScreenYcoordToModelYCoord(int screenYCoordinate)
        {
            double screenRange = (panel.Size.Height - padding) - padding;
            double fileRange = maxYCoord - minYCoord;
            return (int)(((screenYCoordinate - padding) * fileRange) / screenRange) + minYCoord;
        }


        private void AddPaths()
        {
            List<Vertex> vertices = astar.Vertices;

            for (int i = 1; i < vertices.Count; i++)
            {
                Vertex v = vertices[i];
                List<int> adjList = v.AdjList;
                foreach (int ajdVertex in adjList)
                {
                    City tailCity = shapes.Find(v.Name) as City;
                    City headCity = shapes.Find(vertices[ajdVertex].Name) as City;
                    AddPath(tailCity, headCity);
                }
            }
        }

        City startCity, endCity;


        private void makeStartCityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startCity != null)
            {
                startCity.Color = Color.Brown;
                panel.Invalidate(startCity.GetLargesetPossibleRegion());
            }
            currentSelectedCity.Color = Color.Cyan;
            startCity = currentSelectedCity;

            panel.Invalidate(startCity.GetLargesetPossibleRegion());
        }

        private void makeEndCityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endCity != null)
            {
                endCity.Color = Color.Brown;
                panel.Invalidate(endCity.GetLargesetPossibleRegion());
            }
            currentSelectedCity.Color = Color.Green;
            endCity = currentSelectedCity;

            panel.Invalidate(endCity.GetLargesetPossibleRegion());
        }

        private void button_GO_Click(object sender, EventArgs e)
        {
            if (StartAndEndCitiesSelected())
            {
                panel.Enabled = false;
                if (firstStep)
                {
                    InitShortestPath();
                }
                timer.Enabled = true;
            }
        }

        bool firstStep = true;
        List<ConsideredPath> consideredPaths;
        List<Vertex> shortestPath;
        int consideredPathIndex = 0;

        private void button_Step_Click(object sender, EventArgs e)
        {
            if (StartAndEndCitiesSelected())
            {
                if (firstStep)
                {
                    panel.Enabled = false;
                    InitShortestPath();
                }
                OneStep();
            }
        }

        private void NotifyUserOfNoPath()
        {
            MessageBox.Show("No path exists between these two cities.");
            ResetMap();
        }

        private bool StartAndEndCitiesSelected()
        {
            if (startCity == null && endCity == null)
            {
                MessageBox.Show("Select start and end cities by right clicking on them and selecting the appropriate menu item.");
                return false;
            }

            if (startCity == null)
            {
                MessageBox.Show("Select start city.");
                return false;
            }

            if (endCity == null)
            {
                MessageBox.Show("Select end city.");
                return false;
            }

            return true;
        }

        private void OneStep()
        {
            if (consideredPathIndex >= consideredPaths.Count)
            {
                timer.Stop();
                if (shortestPath.Count == 0)
                {
                    NotifyUserOfNoPath();
                }
                ShowFinalPath();
                SetFinalPathState();
            }
            else
            {
                ColorPath(consideredPathIndex);
                UpdateText(consideredPathIndex++);
            }
        }

        private void UpdateText(int index)
        {
            ConsideredPath path = consideredPaths[index];

            if (!path.CurrentlyOptimal) 
            {
                string pathText = path.Head + Environment.NewLine + "G Score: " + String.Format("{0:0.##}", path.GScore) + 
                    Environment.NewLine + "H Score: " + String.Format("{0:0.##}", path.HScore) + Environment.NewLine + 
                    "F Score: " + String.Format("{0:0.##}", path.FScore) + Environment.NewLine + Environment.NewLine +
                    Environment.NewLine;

                textBox_TextRepresentation.Text += pathText;
            }
        }

        private void SetFinalPathState()
        {
            button_Step.Enabled = false;
            button_GO.Enabled = false;
            button_FinalPath.Enabled = false;
            timer.Enabled = false;
            panel.Enabled = true;
            button_Reset.Enabled = true;
        }

        private void ColorPath(int index)
        {
            ConsideredPath path = consideredPaths[index];

            Shape[] arrow = shapes.ArrowsBetweenCities(path.Tail, path.Head);
            if (path.CurrentlyOptimal)
            {
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] != null)
                    {
                        arrow[i].Color = Color.FromArgb(21, 253, 3);
                    }
                }
            }
            else
            {
                for (int i = 0; i < arrow.Length; i++)
                {
                    if (arrow[i] != null)
                    {
                        arrow[i].Color = Color.FromArgb(3, 111, 221);
                    }
                }
            }
            panel.Invalidate(arrow[0].GetLargesetPossibleRegion());
            
        }

        private void InitShortestPath()
        {
            shortestPath = astar.getShortestPath(startCity.Name, endCity.Name, new string[] { }, heuristic);
            consideredPaths = astar.ConsideredPath;
            firstStep = false;
        }

        private void ShowFinalPath()
        {
            AllArrowsBlack();

            for (int i = 0; i < shortestPath.Count - 1; i++)
            {
                Shape[] arrow = shapes.ArrowsBetweenCities(shortestPath[i].Name, shortestPath[i + 1].Name);
                for (int j = 0; j < arrow.Length; j++)
                {
                    if (arrow[j] != null)
                    {
                        arrow[j].Color = Color.FromArgb(21, 253, 3);
                    }
                }
            }

            panel.Invalidate();
        }

        private void AllArrowsBlack()
        {
            foreach (Shape shape in shapes)
            {
                Arrow arrow = shape as Arrow;
                if (arrow != null)
                {
                    arrow.Color = Color.Black;
                }
            }
        }

        IHeurisic heuristic = new StraightLine();

        private void radioButton_StraightLine_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_StraightLine.Checked)
            {
                heuristic = new StraightLine();
            }
        }

        private void radioButton_OneHop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_OneHop.Checked)
            {
                heuristic = new OneHop();
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            ResetMap();
        }

        private void ResetMap()
        {
            firstStep = true;
            AllArrowsBlack();
            panel.Invalidate();
            button_GO.Enabled = true;
            button_Step.Enabled = true;
            button_FinalPath.Enabled = true;
            button_Reset.Enabled = false;
            consideredPathIndex = 0;
            textBox_TextRepresentation.Text = "";
        }

        int delay = 200;
        int Delay
        {
            get { return delay; }
            set
            {
                if (value < MinDelay) value = MinDelay;
                if (value > MaxDelay) value = MaxDelay;
                delay = value;
                timer.Interval = delay;
            }
        }

        const int MaxDelay = 1000;
        const int MinDelay = 50;
        const int Delta = 25;

        private void button_Slower_Click(object sender, EventArgs e)
        {
            if (Delay == MinDelay)
            {
                button_Faster.Enabled = true;
            }

            Delay += Delta;

            if (Delay == MaxDelay)
            {
                button_Slower.Enabled = false;
            }
        }

        private void button_Faster_Click(object sender, EventArgs e)
        {
            if (Delay == MaxDelay)
            {
                button_Slower.Enabled = true;
            }

            Delay -= Delta;

            if (Delay == MinDelay)
            {
                button_Faster.Enabled = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            OneStep();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Help().Show();
        }

        private void button_FinalPath_Click(object sender, EventArgs e)
        {
            if (StartAndEndCitiesSelected())
            {
                InitShortestPath();
                if (shortestPath.Count == 0)
                {
                    NotifyUserOfNoPath();
                }
                ShowFinalPath();
                ShowRemainingText();
                SetFinalPathState();
            }
        }

        private void ShowRemainingText()
        {
            for (int i = consideredPathIndex; i < consideredPaths.Count; i++)
            {
                UpdateText(i);
            }
            textBox_TextRepresentation.SelectionStart = textBox_TextRepresentation.Text.Length;
        }

        private void textBox_TextRepresentation_TextChanged(object sender, EventArgs e)
        {
            textBox_TextRepresentation.SelectionStart = textBox_TextRepresentation.Text.Length;
            textBox_TextRepresentation.ScrollToCaret();
            textBox_TextRepresentation.Refresh();
        }
    }
}