import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TarjetaService } from '../../services/tarjeta.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tarjeta-credito',
  templateUrl: './tarjeta-credito.component.html',
  styleUrls: ['./tarjeta-credito.component.css']
})

export class TarjetaCreditoComponent implements OnInit {

  listTarjetas: any[] = [];
  action = "agregar tarjeta";
  form: FormGroup;
  numeroTarjetaInicio: any = "";

  constructor(private fb: FormBuilder, 
    private toastr: ToastrService,
    private _tarjetasService: TarjetaService) { 

    this.form = this.fb.group({
      titular: ["", Validators.required],
      numeroTarjeta: ["", [Validators.required, Validators.maxLength(16), Validators.minLength(16)]],
      fechaExpiracion: ["", [Validators.required, Validators.maxLength(5), Validators.minLength(5)]],
      cvv: ["", [Validators.required, Validators.maxLength(3), Validators.minLength(3)]]
    })


  }

  ngOnInit(): void {

    this.obtenerTarjetas();
  }

  obtenerTarjetas(){

    this._tarjetasService.getListTarjetas().subscribe(data => {

      console.log(data);
      this.listTarjetas = data;

    }, error => {

      console.log(error);

    });


  }

  agregarTarjeta(){


    if(this.verificaExistencia(this.numeroTarjetaInicio)){
      
      
      const tarjeta: any = {
        titular: this.form.get("titular")?.value,
        numeroTarjeta: this.form.get("numeroTarjeta")?.value,
        fechaExpiracion: this.form.get("fechaExpiracion")?.value,
        cvv: this.form.get("cvv")?.value
      };

      this._tarjetasService.editTarjeta(this.numeroTarjetaInicio, tarjeta).subscribe(
        data => {

          this.toastr.success('La tarjeta con numero ' + data.numeroTarjeta + ' se ha editado con exito', 'Editar Tarjeta');
          this.obtenerTarjetas();
        },
        error => {
          this.toastr.error('Error al editar tarjeta', 'Editar Tarjeta');
        }
      );


    }else{

      const tarjeta: any = {
        titular: this.form.get("titular")?.value,
        numeroTarjeta: this.form.get("numeroTarjeta")?.value,
        fechaExpiracion: this.form.get("fechaExpiracion")?.value,
        cvv: this.form.get("cvv")?.value
      };

      this._tarjetasService.saveTarjeta(tarjeta).subscribe(
        data => {

          this.toastr.success('La tarjeta con numero ' + data.numeroTarjeta + ' se ha registrado con exito', 'Registrar Tarjeta');
          this.obtenerTarjetas();
        },
        error => {

          this.toastr.error('Error al registrar tarjeta', 'Registrar Tarjeta');

        }
      );
      
    }

    this.numeroTarjetaInicio="";
    this.action = "agregar tarjeta";
    document.getElementById("divCancelar")!.style.display = "none";
    this.form.reset();

  }

  eliminarTarjeta(numeroTarjeta: number){


    Swal.fire({
      title: '¿Estas seguro de eliminar la tarjeta?',
      text: "Esta acción no se podra revertir",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: "Cancelar",
      confirmButtonText: 'Eliminar Tarjeta'
    }).then((result) => {
      if (result.isConfirmed) {
        
        this._tarjetasService.deleteTarjeta(numeroTarjeta).subscribe(
          data => {

            this.toastr.success('La tarjeta fue eliminada con exito', 'Eliminar Tarjeta');
            this.obtenerTarjetas();

          },
          error => {

            this.toastr.error('Error al eliminar tarjeta', 'Eliminar Tarjeta');
          }
        );

      }else{
        this.toastr.success('Acción Cancelada', 'Eliminar Tarjeta');

      }
    });

    
    this.form.reset();
    
    
  }

  editarTarjeta(index: number){


    this.action = "editar tarjeta";

    this.form.setValue({

      "titular": this.listTarjetas[index].titular,
      "numeroTarjeta": this.listTarjetas[index].numeroTarjeta,
      "fechaExpiracion": this.listTarjetas[index].fechaExpiracion,
      "cvv": this.listTarjetas[index].cvv
    });
    
    this.numeroTarjetaInicio = this.form.get("numeroTarjeta")?.value;
    
    document.getElementById("divCancelar")!.style.display = "block";
    
  }

  cancelar(){

    this.action = "agregar tarjeta";
    document.getElementById("divCancelar")!.style.display = "none";
    this.form.reset();

  }

  private verificaExistencia(numeroTarjeta: string): boolean{

    

    let existThis: boolean = false;

    for(let tarjeta of this.listTarjetas){


      if(tarjeta.numeroTarjeta == numeroTarjeta){
        existThis = true;
        break;
      }else{
        existThis=false;
        
      }

    }

    return existThis;

  }

  private obtenIndex(numeroTarjeta: string): number{

    let contador: number = -1;

    for(let tarjeta of this.listTarjetas){

      contador+=1;

      if(tarjeta.numeroTarjeta == numeroTarjeta)
        break;
        
      

    }

    return contador;

  }


}
