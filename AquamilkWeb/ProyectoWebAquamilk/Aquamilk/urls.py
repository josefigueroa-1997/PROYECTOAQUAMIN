from django.urls import path
from . import comunaview
from . import usuarioview,rolview

urlpatterns = [

    path('comunas/',comunaview.obtenercomunas,name="obtener_comunas"),
    path('usuarios/',usuarioview.mantenedor_usuarios,name="usuarios"),
    path('recuperarusuarios/',usuarioview.recuperarusuarios,name="recuperar_usuarios"),
    path('actualizarusuario/<int:usuario_id>/', usuarioview.actualizarusuario, name="actualizar_usuario"),
    path('roles/',rolview.obtenerroles,name="obtener_roles"),
]