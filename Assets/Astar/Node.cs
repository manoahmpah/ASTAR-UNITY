
using UnityEngine;

namespace Astar
{
    public class Node
    {
        public int GridX { get; }
        public int GridY { get; }
        private readonly Vector2 _worldPosition;
        

        public Node(int gridX, int gridY, Vector2 worldPosition)
        { 
            GridX = gridX;
            GridY = gridY;
            _worldPosition = worldPosition;
        }
        
        public Vector2 WorldCoordinateCoordinates()
        {
            return _worldPosition;
        }
        
    }
}