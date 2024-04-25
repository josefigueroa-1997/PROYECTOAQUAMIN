from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse,HttpResponse,HttpResponseBadRequest
from django.core.paginator import Paginator, EmptyPage, PageNotAnInteger
import json


def obteneruta(request,id=None):
    context = {}
    template_name= "ruta.html"
    if id is None:
        id=1
    
    api_url = f"http://www.aquamilk.somee.com/Despacho/GetRuta/{id}"
    try:
        response = requests.get(api_url)
        if response.status_code == 200:
            ruta = response.json()
            print (ruta)
            context['ruta'] = ruta
            return render(request,template_name,context)
        else:
            mensaje_error = f'Error al obtener ventas: {response.status_code}'
            print(mensaje_error)
            return render(request, template_name, {'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return render(request, template_name, {'error': mensaje_error})
    