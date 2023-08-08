using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public  class GroundDecoratorBehavior:AthenaMonoBehavior
{
    [SerializeField]
    private float _subdivisionsX = 10;
    [SerializeField]
    private float _subdivisionsY = 10;

    [SerializeField] private GameObject _buildingPrefab; 

    [SerializeField] private List<Texture2D> _buildingTextures;
    private static readonly int Sprite1 = Shader.PropertyToID("_Sprite");

    protected override void Start()
    {
        SafeAssigned(_buildingPrefab);

        BuildBuildings();
    }

    private void BuildBuildings()
    {
        //var center= transform.position;
        //var upperLeft =new Vector3(transform.position.x-transform.localScale.x/2, transform.position.y - transform.localScale.y / 2);


        for (var i = 0; i < _subdivisionsX; i++)
        {
            for (var j = 0; j < _subdivisionsY; j++)
            {
                var point = new Vector3(.5f - i / _subdivisionsX, .5f - j / _subdivisionsY, transform.position.z);
                var transformedPoint = transform.TransformPoint(point);

                Collider2D[] result = new Collider2D[1];
                Physics2D.OverlapCircleNonAlloc(transformedPoint, 1, result, _gameManager.Buildings);//using x here, magnitude is giving an odd result

                if (result[0] == null)
                {
                 

                    var building = Instantiate(_buildingPrefab, transformedPoint, Quaternion.identity, transform);
                    var texture = _buildingTextures.GetRandom();
                    var scaleAdjust = texture.width / 350f;

                    building.transform.localScale = new Vector3(building.transform.localScale.x / transform.localScale.x* scaleAdjust, building.transform.localScale.y / transform.localScale.y* scaleAdjust,
                        1 / transform.localScale.z);
                    building.GetComponent<Renderer>().material.SetTexture(Sprite1, texture);

                    building.SetActive(true);
                }
            }
        }
    }
}

