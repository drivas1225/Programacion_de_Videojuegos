
#ifdef _INTELLISENSE_
void __syncthreads();
#endif


#include <stdio.h>
#include <iostream>

#include "cuda.h"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <device_functions.h>

#define DIMBLOCKX 32


template<class T>
void print_vector(T* v, unsigned s) {
    for (unsigned i = 0; i < s; i++) {
        std::cout << v[i] << " ";
    }
    std::cout << "\n";
}


__global__ void SumaColMatrizKernel(int M, float* Md, float* Nd) {
    __shared__ float Nds[DIMBLOCKX];
    float Pvalue = 0;
    int columna = blockIdx.x;
    int pasos = M / blockDim.x;
    int posIni = columna * M + threadIdx.x * pasos;
    for (int k = 0; k < pasos; ++k) {
        Pvalue = Pvalue + Md[posIni + k];
    }
    Nds[threadIdx.x] = Pvalue; 
    __syncthreads();
    if (threadIdx.x == 0) {
        for (int i = 1; i < blockDim.x; ++i) {
            Nds[0] = Nds[0] + Nds[i];
        }
        Nd[blockIdx.x] = Nds[0];
    }
}


void SumaColMatriz(int M, int N, int* Mh, int* Nh) {
    int size = M * N * sizeof(float), size2 = N * sizeof(float);
    float* Md, * Nd;
    // Allocate en device
    cudaMalloc(&Md, size);
    cudaMalloc(&Nd, size2);
    // Inicializo matrices en el device
    // Inicializo matrices en el device
    cudaMemcpy(Md, Mh, size, cudaMemcpyHostToDevice);
    cudaMemset(Nd, 0, size2);

    // Invocar el kernel que suma en GPU

    // configuración de la ejecución
    int chunk = 32;
    dim3 tamGrid(N, 1); //Grid dimensión
    dim3 tamBlock(M / chunk,1, 1); //Block dimensión
    // lanzamiento del kernel
    SumaColMatrizKernel <<<tamGrid, tamBlock >>> (M, Md, Nd);

    // Traer resultado;
    cudaMemcpy(Nh, Nd, size2, cudaMemcpyDeviceToHost);
    // Free matrices en device
}

int main()
{
    int M = 32;
    int N = 64;

    int* Mh = new int[M * N];
    int* Rh = new int[N];

    for (int i = 0; i < M; i++) {
        for (int j = 0; j < N; j++) {
            Mh[(i * N) + j] = j;
        }
    }


    SumaColMatriz(M, N, Mh, Rh);
    print_vector(Rh, N);

    delete[] Mh;
    delete[] Rh;



    return 0;
}

