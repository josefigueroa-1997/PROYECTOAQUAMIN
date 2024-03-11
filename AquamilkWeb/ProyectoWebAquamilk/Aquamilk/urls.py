from django.urls import path
from . import views


urlpatterns = [

    path('comunas/',views.obtenercomunas,name="comunas"),


]