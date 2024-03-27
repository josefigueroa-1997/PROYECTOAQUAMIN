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
            return JsonResponse({'productos':productos})
        else:
            mensaje_error = f'Error al obtener los productos.Código de estado:{response.status_code}'
            return JsonResponse({'error': mensaje_error},status=response.status_code)
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return JsonResponse({'error': mensaje_error},status=500)
