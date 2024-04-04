from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse,HttpResponse,HttpResponseBadRequest
import json


def recuperarproductos(request):
    url = "http://www.aquamilk.somee.com/Producto/GetProductos"

    try:
        response = requests.get(url)
        if response.status_code == 200:
            productos = response.json()
            productos_precio = []
            for producto in productos:
                precio = producto.get('precio',None)
                if precio is not None:
                    productos_precio.append({
                        'id':producto['id'],
                        'tipo_Producto':producto['tipo_Producto'],
                        'precio':producto['precio'],
                    })
            return JsonResponse({'productos':productos_precio})
        else:
            mensaje_error = f'Error al obtener los productos.Código de estado:{response.status_code}'
            return JsonResponse({'error': mensaje_error},status=response.status_code)
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return JsonResponse({'error': mensaje_error},status=500)
