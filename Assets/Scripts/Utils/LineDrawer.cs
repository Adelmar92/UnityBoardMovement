using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private GameObject _pointerLine;
    /*TODO ESTO ES PARA DIBUJAR UNA CURBA NOMAS*/
    public void DrawCurveBetweenObjects(GameObject startObject, GameObject endObject)
    {
        Destroy(_pointerLine);
        // Crear un nuevo objeto de línea
        _pointerLine = new GameObject("Line");

        // Añadir el componente LineRenderer al objeto
        LineRenderer lineRenderer = _pointerLine.AddComponent<LineRenderer>();

        // Definir los puntos de control de la curva de Bezier
        Vector3[] controlPoints = CalculateBezierControlPoints(startObject, endObject);

        // Calcular los puntos de la curva de Bezier
        Vector3[] curvePoints = GenerateCurvePoints(controlPoints, 50);

        // Establecer los puntos de la línea con la curva
        lineRenderer.positionCount = curvePoints.Length;
        lineRenderer.SetPositions(curvePoints);

        // Opcional: Personalizar la apariencia de la línea
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.red;
    }

    private Vector3[] CalculateBezierControlPoints(GameObject startObject, GameObject endObject)
    {
        // Calcular los puntos de control para la curva de Bezier

        // Punto de inicio
        Vector3 startPoint = startObject.transform.position;

        // Punto medio entre el inicio y el fin
        Vector3 midPoint = (startPoint + endObject.transform.position) / 2f;
        midPoint.y += 5f; // Ajusta la altura del arco modificando este valor

        // Punto de fin
        Vector3 endPoint = endObject.transform.position;

        // Puntos de control
        Vector3 controlPoint1 = Vector3.Lerp(startPoint, midPoint, 0.33f);
        Vector3 controlPoint2 = Vector3.Lerp(midPoint, endPoint, 0.66f);

        return new Vector3[] { startPoint, controlPoint1, controlPoint2, endPoint };
    }

    private Vector3[] GenerateCurvePoints(Vector3[] controlPoints, int pointCount)
    {
        // Generar los puntos de la curva utilizando interpolación lineal

        Vector3[] curvePoints = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            curvePoints[i] = CalculateBezierPoint(controlPoints, t);
        }

        return curvePoints;
    }

    private Vector3 CalculateBezierPoint(Vector3[] controlPoints, float t)
    {
        // Calcular un punto en la curva de Bezier utilizando los puntos de control y el parámetro t

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * controlPoints[0]; // P0
        point += 3 * uu * t * controlPoints[1]; // 3P1
        point += 3 * u * tt * controlPoints[2]; // 3P2
        point += ttt * controlPoints[3]; // P3

        return point;
    }

    public void RemoveLine() {
        Destroy(_pointerLine);
    }
}
