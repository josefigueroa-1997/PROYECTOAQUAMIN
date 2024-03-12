from django.urls import path
from . import views
from . import usuarioview

urlpatterns = [

    path('comunas/',views.obtenercomunas,name="comunas"),
    path('usuarios/',usuarioview.obtenerusuarios,name="usuarios")

]