from django.urls import path
from . import comunaview
from . import usuarioview,rolview

urlpatterns = [

    path('comunas/',comunaview.obtenercomunas,name="obtener_comunas"),
    path('usuarios/',usuarioview.obtenerusuarios,name="usuarios"),
    path('roles/',rolview.obtenerroles,name="obtener_roles"),
]