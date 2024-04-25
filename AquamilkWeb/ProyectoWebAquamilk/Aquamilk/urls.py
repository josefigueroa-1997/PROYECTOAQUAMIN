from django.urls import path
from . import usuarioview,rolview,rutaview,comunaview,ventasview,productoview

urlpatterns = [

    path('comunas/',comunaview.obtenercomunas,name="obtener_comunas"),
    path('usuarios/',usuarioview.mantenedor_usuarios,name="usuarios"),
    path('actualizarusuario/<int:usuario_id>/', usuarioview.actualizarusuario, name="actualizar_usuario"),
    path('eliminarusuario/',usuarioview.eliminarusuario,name="eliminar_usuario"),
    path('roles/',rolview.obtenerroles,name="obtener_roles"),
    path('login/',usuarioview.login,name="login"),
    path('logout/',usuarioview.logout,name="logout"),
    path('ruta/',rutaview.obteneruta,name="ruta"),
    path('recuperarusuarios/',usuarioview.recuperarusuarios,name="recuperarusuarios"),
    path('recuperarproductos/',productoview.recuperarproductos,name="recuperarproductos"),
    path('ventas/',ventasview.mantenedor_ventas,name="ventas"),
    path('historial/',ventasview.historialventas,name="historial"),
    path('eliminar_venta/',ventasview.eliminar_venta,name="eliminar_venta"),
]