from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse


#OBTENER  COMUNAS


def obtenercomunas(request):
    url = "http://www.aquamilk.somee.com/Comuna/GetComunas"

    try:
        response = requests.get(url)
        if response.status_code == 200:
            comunas = response.json()
            return JsonResponse({'comunas':comunas})
        else:
            mensaje_error = f'Error al obtener las comunas.Código de estado:{response.status_code}'
            return JsonResponse({'error': mensaje_error},status=response.status_code)
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return JsonResponse({'error': mensaje_error},status=500)




