from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse,HttpResponseBadRequest

#AGREGAR USUARIO
def agregar_usuario(request):
    url_apipost = "http://www.aquamilk.somee.com/Usuario/AddUsuario"

    if request.method == 'POST':
        nombre = request.POST.get('nombre')
        telefono = request.POST.get('telefono')
        calle = request.POST.get('calle')
        numero = int(request.POST.get('numero'))
        comuna = int(request.POST.get('comuna'))
        correo = request.POST.get('correo')
        contraseña = request.POST.get('contraseña')
        IdRol = int(request.POST.get('rol'))
        usuario = {
            'Nombre': nombre,
            'Telefono': telefono,
            'Calle': calle,
            'Numero': numero,
            'IdComuna': comuna,
            'Correo': correo,
            'Contrasena': contraseña,
            'IdRol': IdRol
        }
        try:
            response = requests.post(url_apipost, json=usuario)
            if response.status_code == 200:
                return redirect('usuarios')
            else:
                mensaje_error = f'Error al añadir un usuario: {response.status_code}'
                return render(request, 'mantenedor_usuarios.html', {'error': mensaje_error})
        except Exception as e:
            mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
            return render(request, 'mantenedor_usuarios.html', {'error': mensaje_error})

#OBTENER USUARIO
def obtenerusuario(request):
    urlapiget = "http://www.aquamilk.somee.com/Usuario/GetUsuarios"
    parametros = {'id': request.GET.get('id'), 'nombre': request.GET.get('nombre')}
    template_name = "mantenedor_usuarios.html"
    try:
        response = requests.get(urlapiget, params=parametros)
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



def recuperarusuarios(request):
    url_api_get = "http://www.aquamilk.somee.com/Usuario/GetUsuarios"
    usuario_id = request.GET.get('id') 
    try:
        response = requests.get(url_api_get, params={'id':usuario_id})
        response.raise_for_status()
        usuarios = response.json()
        return JsonResponse(usuarios, safe=False)
    except requests.RequestException as e:
        mensaje_error = f'Error al obtener usuarios: {str(e)}'
        return JsonResponse({'error': mensaje_error}, status=500)



# ACTUALIZAR DATOS USUARIOS
    
def actualizarusuario(request,usuario_id):
    url_api_put = f"http://www.aquamilk.somee.com/Usuario/UpdateUsuario/{usuario_id}"
    url_api_get = "http://www.aquamilk.somee.com/Usuario/GetUsuarios"
    template_name="actualizarusuario.html"

    if request.method == 'POST':
        nombre = request.POST.get('nombreEdicion')
        telefono = request.POST.get('telefonoEdicion')
        calle = request.POST.get('calleEdicion')
        numero = int(request.POST.get('numeroEdicion'))
        comuna = int(request.POST.get('comunaEdicion'))
        Idireccion = int(request.POST.get('IdireccionEdicion'))
        usuario = {
            'Nombre': nombre,
            'Telefono': telefono,
            'Calle': calle,
            'Numero': numero,
            'IdComuna': comuna,
            'IDireccion':Idireccion,
            
        }
        try:
            response = requests.put(url_api_put, json=usuario)
            if response.status_code == 200:
                return redirect('usuarios')
            else:
                mensaje_error = f'Error al actualizar un usuario: {response.status_code}'
                return render(request, template_name, {'error': mensaje_error})
        except Exception as e:
            mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
            return render(request, template_name, {'error': mensaje_error})
    try:
        response = requests.get(url_api_get,params={'id':usuario_id})
        if response.status_code == 200:
            usuario = response.json()
            context = {'usuario':usuario}
            return render(request,template_name,context)
        else:
            mensaje_error = f'Error al obtener usuarios: {response.status_code}'
            return render(request, template_name, {'error': mensaje_error})
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        return render(request, template_name, {'error': mensaje_error})     
    
    
    
   
#MANTENEDOR USUARIOS

def mantenedor_usuarios(request):
    if request.POST:
        return agregar_usuario(request)
        
    else:
        return obtenerusuario(request)



    