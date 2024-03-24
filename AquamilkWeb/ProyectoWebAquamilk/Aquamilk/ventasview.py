from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse



#OBTENER VENTAS 

def obtenerventas(request,idventa=None,tipoventa=None,fecha=None,idsector=None):
    context = {}
    template_name = "ventas.html"
    url_get = "http://www.aquamilk.somee.com/Venta/GetVentas"
    if tipoventa is None:
        tipoventa = "Planta"
    params = {
        'id':idventa,
        'tipoventa':tipoventa,
        'fecha':fecha,
        'idsector':idsector
    }
    try:
        response = requests.get(url_get,params=params)
        if response.status_code == 200:
            ventas = response.json()
            context['ventas'] = ventas
            context['tipoventa'] = tipoventa
            return render(request,template_name,context)
        else:
            mensaje_error = f'Error al obtener ventas: {response.status_code}'
            return render(request, template_name, {'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return render(request, template_name, {'error': mensaje_error})


def historialventasusuario(request):
    url_api = "http://www.aquamilk.somee.com/Venta/GetHistorialCliente"
    parametros = {'idusuario':request.GET.get('idusuario'),'fecha':request.GET.get('fecha')}
    try:
        response = requests.get(url_api,params=parametros)
        if response.status_code == 200:
            historial = response.json()
            return JsonResponse(historial,safe=False)
        else:
            mensaje_error = f'Error al obtener usuarios: {response.status_code}'
            return JsonResponse({'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return JsonResponse({'error': mensaje_error})