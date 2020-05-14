
	
#include <stdio.h>
#include <iostream>

#include "cuda.h"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"


using namespace std;

//DEVICE

__global__ void kernelVector_x_constant(float* arr, int n, int k)
{
	//Obtengo el indice del hilo fisico
	int idx = blockIdx.x * blockDim.x + threadIdx.x;

	//Mientras el hilo sea valido para la operacion
	if (idx < n)
	{
		//Multiplico el elemento por la constante
		arr[idx] = arr[idx] * k;
	}
}

__global__ void KernelVector_add_constant(float* arr, float* res , int n, int c) 
{
	int idx = blockIdx.x * blockDim.x + threadIdx.x;

	if (idx < n)
	{
		res[idx] = arr[idx] + c;
	}
}

__global__ void KernelVector_sub_constant(float* arr, float* res, int n, int c)
{
	int idx = blockIdx.x * blockDim.x + threadIdx.x;

	if (idx < n)
	{
		res[idx] = arr[idx] - c;
	}
}

__global__ void KernelVector_add_vector(float *arr1 , float *arr2 , float *res  , int n )
{
	int idx = blockIdx.x * blockDim.x + threadIdx.x;

	if (idx < n)
	{
		res[idx] = arr1[idx] + arr2[idx];
	}
}

__global__ void KernelVector_sub_vector(float* arr1, float* arr2, float* res, int n)
{
	int idx = blockIdx.x * blockDim.x + threadIdx.x;

	if (idx < n)
	{
		res[idx] = arr1[idx] - arr2[idx];
	}
}

//HOST
int main()
{
	int size = 1000000;
	//Separo memoria en la RAM del HOST
	float* arrA;
	float* arrB;
	float e = 5;
	float* res;

	cudaMallocManaged(&arrA, size * sizeof(float));
	cudaMallocManaged(&arrB, size * sizeof(float));
	cudaMallocManaged(&res , size * sizeof(float));

	for (int i = 0; i < size; i++) {
		arrA[i] = i;
		arrB[i] = i;
		res[i] = 0;
	}


	///////////////////////// EJECUTO EL KERNEL DE CUDA ////////////////////////////
	//////// 512 Hilos
	//////// ceil(1000000/512) Bloques
	//kernelVector_x_constant <<< ceil(size / 512.0), 512 >>> (arr_DEVICE, size, 65);
	
	//// tarea ////
	KernelVector_add_constant <<< ceil(size / 512.0), 512 >>> (arrA,res,size,e);
	//KernelVector_sub_constant <<< ceil(size / 512.0), 512 >>> (arrA, res, size, e);
	
	//KernelVector_add_vector <<< ceil(size / 512.0), 512 >>> (arrA, arrB, res, size);
	//KernelVector_sub_vector <<< ceil(size / 512.0), 512 >>> (arrA, arrB, res, size);


	//Fuerzo una llamada Sincrona
	cudaThreadSynchronize();

	for (int index = 0; index < size; index++)
	{
		cout << res[index] << " ; ";
	}



	cudaFree(arrA);
	cudaFree(arrB);
	cudaFree(res);

	return 0;
}