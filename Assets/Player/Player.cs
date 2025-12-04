using System.Collections; // Nécessaire pour IEnumerator
using System.Collections.Generic;
using Astar;
using UnityEngine;

namespace Player
{
    public class Main : MonoBehaviour
    {
        public Graph graph;
        public Vector2 worldEndPosition = new Vector2(-5, -2);

        void Start()
        {
            
            Vector2 worldStartPosition = new Vector2(transform.position.x, transform.position.y);
            
            List<Vector2> path = graph.Path(worldStartPosition, worldEndPosition);
            
            if (path != null && path.Count > 0)
            {
                print(string.Join(" -> ", path));
                // MODIFICATION 1 : On lance la méthode Move comme une Coroutine
                StartCoroutine(MoveSequence(path));
            }
            
        }

        // MODIFICATION 2 : Move devient une Coroutine (IEnumerator)
        private IEnumerator MoveSequence(List<Vector2> path)
        {
            foreach (var targetPosition in path)
            {
                // MODIFICATION 3 : "yield return" attend que MoveCoroutine soit fini
                // avant de passer à l'itération suivante de la boucle.
                yield return StartCoroutine(MoveCoroutine(targetPosition));
            }
            
            print("Arrivée à destination finale !");
        }

        private IEnumerator MoveCoroutine(Vector2 targetPosition)
        {
            Vector2 startPosition = transform.position;
            // Note: Assurez-vous que la vitesse est constante. 
            // Ici * 0.5f signifie que plus c'est loin, plus c'est long (vitesse constante env. 2 unités/sec)
            float duration = Vector2.Distance(startPosition, targetPosition) * 0.5f; 
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}