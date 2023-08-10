using UnityEngine;
using UnityEngine.UIElements;

namespace UIMeshes
{
  public class HexagonMesh
  {
    private readonly float _radius;
    private readonly Color _color;
    private bool _isDirty;
    public Vertex[] Vertices { get; private set; }
    public static ushort[] indices = new ushort[] {
      0, 1, 2,
      0, 2, 3,
      0, 3, 4,
      0, 4, 5,
      0, 5, 6,
      0, 6, 1
    };
    public HexagonMesh(float radius, Color color)
    {
      _radius = radius;
      _color = color;
      _isDirty = true;
    }
    public void UpdateMesh()
    {
      if (!_isDirty) return;
      Vertices = new Vertex[7];
      Vertices[0] = new Vertex
      {
        position = new Vector3(_radius, _radius, 0),
        tint = _color,
        uv = new Vector2(0.5f, 0.5f)
      };
      Debug.Log(Vertices[0].uv);

      var angle = 0f;
      var step = Mathf.PI * 2 / 6;
      for (var i = 1; i < 7; i++)
      {
        var x = Mathf.Cos(angle) * _radius;
        var y = Mathf.Sin(angle) * _radius;
        Vertices[i] = new Vertex
        {
          position = new Vector3(x + _radius, y + _radius, 0f),
          tint = _color,
        };
        Vertices[i].uv = new Vector2(Vertices[i].position.x / _radius / 2, Vertices[i].position.y / _radius / 2);
        Debug.Log(Vertices[i].position);
        Debug.Log(Vertices[i].uv);
        angle += step;
      }
      _isDirty = false;
    }
  }
}