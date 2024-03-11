from django.shortcuts import render,redirect
import requests



#OBTENER  COMUNAS


def obtenercomunas(request):
    url = "http://www.aquamilk.somee.com/Comuna/GetComunas"

    try:
        response = requests.get(url)
        if response.status_code == 200:
            comunas = response.json()
            context = {'comunas':comunas}
            template_name = "comunas.html"
            return render(request,template_name,context)
        else:
            mensaje_error = f'Error al obtener las comunas.Código de estado:{response.status_code}'
            return render(request, template_name, {'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return render(request, template_name, {'error': mensaje_error})




