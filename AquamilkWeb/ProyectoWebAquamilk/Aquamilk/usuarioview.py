from django.shortcuts import render
import requests


#OBTENER USUARIOS

def obtenerusuarios(request):
    urlapi = "http://www.aquamilk.somee.com/Usuario/GetUsuarios"
    parametros = {'id': request.GET.get('id'), 'nombre': request.GET.get('nombre')}
    template_name = "mantenedor_usuarios.html"
    try:
        response = requests.get(urlapi, params=parametros)
        if response.status_code == 200:
            usuarios = response.json()
            context = {'usuarios': usuarios}
            return render(request, template_name, context)
        else:
            mensaje_error = f'Error al obtener usuarios: {response.status_code}'
            return render(request, template_name, {'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return render(request, template_name, {'error': mensaje_error})
