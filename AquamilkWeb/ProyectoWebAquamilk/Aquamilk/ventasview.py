from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse
from datetime import datetime


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



def historialventas(request):
    context = {}
    template_name ="historial.html"
    url_api = "http://www.aquamilk.somee.com/Venta/GetHistorialCliente"
    idusuario = request.GET.get('idusuario',None)
    fecha = request.GET.get('fecha',None)
    if idusuario is None or fecha is None:
        context['mensaje_error'] = 'Los parámetros idusuario y fecha son necesarios'
        return render(request,'error.html',context)
    try:
        params = {'idusuario': idusuario, 'fecha': fecha}
        response = requests.get(url_api,params=params)
        if response.status_code == 200:
            historial = response.json()
            context['historial'] = historial
            return render(request,template_name,context)
        else:
            mensaje_error = f'Error al obtener usuarios: {response.status_code}'
            context['mensaje_error'] = mensaje_error
            return render(request,template_name,context)
    except Exception as e:
        mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
        context['mensaje_error'] = mensaje_error
        return render(request,template_name,context)


def agendar_venta(request):
    template_name = "ventas.html"
    api_url = "http://www.aquamilk.somee.com/Venta/AddVenta"
    if request.POST:
        IdUsuario = int(request.POST.get('usuario'))
        IdProducto = int(request.POST.get('producto'))
        Estado_Pago = request.POST.get('estadoPago')
        Metodo_Pago = request.POST.get('metodo')
        Fecha = request.POST.get('fecha')
        fecha_convertida = datetime.strptime(Fecha, '%Y-%m-%d').strftime("%Y-%m-%d")
        Tipo_Venta = request.POST.get('tipodeventa')
        Detalle = "hola"
        if Tipo_Venta == "Hogar":
            Prioridad = int(request.POST.get('prioridad'))
        else:
            Prioridad = None
        Cantidad = int(request.POST.get('cantidad'))

        venta_data = {
            'IdUsuario':IdUsuario,
            'IdProducto': IdProducto,
            'Estado_Pago':Estado_Pago,
            'Metodo_Pago':Metodo_Pago,
            'Fecha':fecha_convertida,
            'Tipo_Venta':Tipo_Venta,
            'Detalle':Detalle,
            'Prioridad':Prioridad,
            'Cantidad':Cantidad
        }
        try:
            response = requests.post(api_url,json=venta_data)
            if response.status_code == 200:
                return redirect('ventas')
            else:
                mensaje_error = f'Error al agregar la venta: {response.status_code}'
                return render(request, template_name, {'mensaje_error': mensaje_error})
        except Exception as e:
            mensaje_error = f'Error al realizar la solicitud HTTP: {e}'
            return render(request, 'error.html', {'mensaje_error': mensaje_error})


def mantenedor_ventas(request):
    if request.POST:
        return agendar_venta(request)
    else:
        tipoventa = request.GET.get('tipoventa')
        if tipoventa == 'Hogar':  
            return obtenerventas(request, tipoventa=tipoventa)
        return obtenerventas(request, tipoventa)