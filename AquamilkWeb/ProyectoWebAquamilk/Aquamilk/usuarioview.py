from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse,HttpResponse,HttpResponseBadRequest
import json
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
                return redirect('usuarios') + '?mensaje=Usuario registro exitosamente'
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
    
    
#ELIMINAR USUARIO

def eliminarusuario(request):
    if request.POST:
        id_usuario = request.POST.get('id_usuario')
        if id_usuario:
            url_api = f"http://www.aquamilk.somee.com/Usuario/DeleteUsuario/{id_usuario}"
            try:
                response = requests.delete(url_api)
                if response.status_code == 200:
                    return HttpResponse("Usuario eliminado con éxito")
                else:
                    return HttpResponseBadRequest("No se pudo eliminar con éxito")
            except requests.exceptions.RequestException as e:
                return HttpResponseBadRequest (f"Hubo un error al hacer la solucion:{e}")
        else:
            return HttpResponseBadRequest("ID de usuario no proporcionado en la solicitud")
    else:
        return HttpResponseBadRequest("La solicitud debe ser de tipo POST")

#MANTENEDOR USUARIOS

def mantenedor_usuarios(request):
    if "idusuario" not in request.session or request.session["idusuario"] is None:
        return redirect('login')
    else:
        if request.session["idrol"] == 1 or request.session["idrol"] == 2:
            if request.POST:
                return agregar_usuario(request)
            else:
                return obtenerusuario(request)
        else:
            return redirect('ruta')

def login(request):
    context = {}
    template_name = "login.html"
    error_message = None
    if request.POST:
        correo = request.POST.get('correo')
        contrasena = request.POST.get('contrasena')
        datos = {
            "Correo":correo,
            "contrasena":contrasena
        }
        url_api = "http://www.aquamilk.somee.com/Usuario/IniciarSesion"
        response = requests.post(url_api,json=datos)
        if response.status_code == 200:
            resultado = response.json()
            if resultado.get("idUsuario",0)>0:
                request.session["idusuario"] = resultado.get("idUsuario",0)
                request.session["idrol"] = resultado.get("idRol",0)
                request.session["nombreusuario"] = resultado.get("nombreUsuario",0)
                if request.session["idrol"] == 1 or request.session["idrol"] == 2:
                    return redirect('usuarios')
                elif request.session["idrol"] == 3:
                    return redirect('ruta')
            else:
               error_message = "No existe un usuario con esas creedenciales"
        else:
            return HttpResponse("Errol al llamar a la api")
    context['error_message'] = error_message
    return render(request,template_name,context)


def logout(request):
    request.session.flush()
    return redirect('login')