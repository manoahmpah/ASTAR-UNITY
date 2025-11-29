using System.Collections.Generic;
using UnityEngine;


namespace Astar
{
    public class Graph : MonoBehaviour
    {
        private List<List<Node>> _grid;
        public int gridXSize = 10;
        public int gridYSize = 10;
        
        private readonly (int, int)[] _neighborDirections = { (0, -1), (0, 1), (-1, 0), (1, 0), (-1, 1), (1, -1), (-1, -1), (1, 1) };
        
        private List<List<int>> _linkCosts;
        private int _pathTravel;
        
        [Header("Gizmos")]
        public bool drawGrid = true;
        public bool drawLinks = true;
        public bool drawBlockedLinks = true;
        public float nodeGizmoSize = 0.1f;
        public Color nodeColor = Color.white;
        public Color linkColor = Color.gray;
        public Color blockedLinkColor = Color.red;
        public Color centerColor = Color.yellow;
        
        public void Awake()
        {
            CreateGrid();
        }

        public List<Vector2> Path(Vector2 worldStartPosition, Vector2 worldEndPosition)
        {
            _pathTravel = 0;
            AddObstacle(0, 1);
            var gridStartPosition = ConvertWorldPositionToGridPosition((int)worldStartPosition.x, (int)worldStartPosition.y);
            var startNode = _grid[(int)gridStartPosition.x][(int)gridStartPosition.y];
            List<Vector2> path = FindPath(startNode, worldEndPosition);
            return path;
        }
        
         private List<Vector2> FindPath(Node startNode, Vector2 worldEndPosition , int costTravel = -1, List<Vector2> listPath = null)
         {
             var (bestNode, cost) = ChooseBestNode(startNode, worldEndPosition);
             listPath ??= new List<Vector2>();
             if (bestNode == null) 
             {
                 Debug.LogWarning("Aucun chemin trouvé ou cul-de-sac atteint.");
                 return listPath;
             }
             _pathTravel += cost;
             listPath.Add(bestNode.WorldCoordinateCoordinates());
             
             if (costTravel == 0) { return listPath; }
             
             if (bestNode.WorldCoordinateCoordinates() == worldEndPosition)
             {
                 Debug.Log("Destination atteinte !");
                 
                 return listPath;
             }
         
             return FindPath(bestNode, worldEndPosition, cost, listPath);
         }
        

        private void CreateGrid()
        {
            _grid = new List<List<Node>>(gridXSize);
            _linkCosts = new List<List<int>>();
            
            for (var xSize = 0; xSize < gridXSize; xSize++)
            {
                _grid.Add(new List<Node>(gridYSize));
                for (var ySize = 0; ySize < gridYSize; ySize++)
                {
                    var worldPosition = new Vector2(xSize - (gridXSize / 2), -(ySize - (gridYSize / 2)));
                    var newNode = new Node(xSize, ySize, worldPosition);
                    _grid[xSize].Add(newNode);
                    _linkCosts.Add(new List<int>());
                    
                    foreach (var (i, j) in _neighborDirections)
                    {
                        
                        var neighborX = newNode.GridX + i;
                        var neighborY = newNode.GridY + j;
        
                        if (neighborX < 0 || neighborX >= gridXSize || neighborY < 0 || neighborY >= gridYSize) 
                        { 
                             _linkCosts[xSize * gridYSize + ySize].Add(0);
                            continue; 
                        }
                        _linkCosts[xSize * gridYSize + ySize].Add(1);
                    }
                }
            }
            
            _linkCosts[0][1] = 999;
        }
        
        private void AddObstacle(int worldX, int worldY)
        {
            var gridPosition = ConvertWorldPositionToGridPosition(worldX, worldY);
            var gridX = (int)gridPosition.x;
            var gridY = (int)gridPosition.y;
            
            // print(gridX + " " + gridY);
            
            // a implémenter
        }
        
        private Vector2 ConvertWorldPositionToGridPosition(int worldX, int worldY)
        {
            var gridX = worldX + (gridXSize / 2);
            var gridY = - worldY + (gridYSize / 2);
            return new Vector2(gridX, gridY);
        }

        private (Node, int) ChooseBestNode(Node node, Vector2 worldEndPosition)
        {
            var endGridPosition = ConvertWorldPositionToGridPosition((int)worldEndPosition.x, (int)worldEndPosition.y);
            var endGridX = (int)endGridPosition.x;
            var endGridY = (int)endGridPosition.y;
            
            if (endGridX < 0 || endGridX >= gridXSize || endGridY < 0 || endGridY >= gridYSize)
            {
                 Debug.LogError("La destination est hors de la grille !");
                 return (null, 0);
            }

            var endingNodeCoordinates = _grid[endGridX][endGridY].WorldCoordinateCoordinates();
            
            Node bestNode = null;
            var bestLinkCost = 0; 
            
            float minTotalCost = float.MaxValue;
            
            for (var index = 0; index < _neighborDirections.Length; index++)
            {
                var i = _neighborDirections[index].Item1;
                var j = _neighborDirections[index].Item2;

                var neighborX = node.GridX + i;
                var neighborY = node.GridY + j;

                // Vérifications de validité (limites de la grille)
                if (neighborX < 0 || neighborX >= gridXSize || neighborY < 0 || neighborY >= gridYSize) continue;
                
                var neighborNode = _grid[neighborX][neighborY];
                
                var currentLinkCost = _linkCosts[node.GridX * gridYSize + node.GridY][index];
                
                var neighborCoords = neighborNode.WorldCoordinateCoordinates();
                
                var dx = neighborCoords.x - endingNodeCoordinates.x;
                var dy = neighborCoords.y - endingNodeCoordinates.y;
                var hCost = Mathf.Sqrt(dx * dx + dy * dy);
                
                // Le coût total est le chemin parcouru + heuristique + coût du saut actuel
                var finalCost = _pathTravel + hCost + currentLinkCost;
                
                if (finalCost < minTotalCost)
                {
                    minTotalCost = finalCost;
                    bestNode = neighborNode;
                    bestLinkCost = currentLinkCost;
                }
            }
            return (bestNode, bestLinkCost);
        }
        
        
          private void OnDrawGizmos()
        {
            // Ensure grid exists in editor
            if (_grid == null || _grid.Count != gridXSize || _grid[0].Count != gridYSize)
            {
                CreateGrid();
            }

            var prevColor = Gizmos.color;

            if (!drawGrid)
            {
                Gizmos.color = prevColor;
                return;
            }

            // Draw center
            Gizmos.color = centerColor;
            Gizmos.DrawWireCube(transform.position, Vector3.one * 0.05f);
            
            for (int x = 0; x < gridXSize; x++)
            {
                for (int y = 0; y < gridYSize; y++)
                {
                    var node = _grid[x][y];
                    var wp = node.WorldCoordinateCoordinates();
                    var pos = new Vector3(wp.x + transform.position.x, wp.y + transform.position.y, transform.position.z);

                    // Draw node
                    Gizmos.color = nodeColor;
                    Gizmos.DrawSphere(pos, nodeGizmoSize);

                    // Draw links to neighbors (only forward to avoid double-drawing)
                    if (drawLinks)
                    {
                        for (int i = 0; i < _neighborDirections.Length; i++)
                        {
                           
                            var dir = _neighborDirections[i];
                            var nx = node.GridX + dir.Item1;
                            var ny = node.GridY + dir.Item2;
                            
                            
                            if (nx < 0 || nx >= gridXSize || ny < 0 || ny >= gridYSize) continue;

                            var neighbor = _grid[nx][ny];
                            var nwp = neighbor.WorldCoordinateCoordinates();
                            var npos = new Vector3(nwp.x + transform.position.x, nwp.y + transform.position.y, transform.position.z);

                            var cost = _linkCosts[node.GridX * gridYSize + node.GridY][i];
                            // print(i + " " + cost);
                            
                            
                            
                            if (cost > 1)
                            {
                                Gizmos.color = blockedLinkColor;
                            } else if (cost == 1)
                            {
                                Gizmos.color = linkColor;
                            }
                            
                            Gizmos.DrawLine(pos, npos);
                        }
                    }
                }
            }

            Gizmos.color = prevColor;
        }
    }
    
}