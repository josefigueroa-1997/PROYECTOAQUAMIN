from django.shortcuts import render,redirect
import requests
from django.http import JsonResponse,HttpResponse,HttpResponseBadRequest
from django.core.paginator import Paginator, EmptyPage, PageNotAnInteger
import json


def obteneruta(request):
    context = {}
    tempplate_name= "ruta.html"
    return render(request,tempplate_name,context)