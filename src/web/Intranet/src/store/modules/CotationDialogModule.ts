//import store from '@/store';
import { Module, VuexModule,Mutation,Action} from 'vuex-module-decorators'

export interface ICotationDialogState {
    dialogState: boolean;
    
}

//console.dir(store);
@Module({stateFactory:true})
export default class CotationDialog extends VuexModule implements ICotationDialogState{

    dialogState = false;

    @Mutation OPEN_CLOSE(value: boolean) { this.dialogState = value; }

    // action 'incr' commits mutation 'increment' when done with return value as payload
    @Action({ commit: 'openclose' }) open() { return true;}
    // action 'decr' commits mutation 'decrement' when done with return value as payload
    @Action({ commit: 'openclose' }) close() { return false; }

    get dialog():boolean {
        return this.dialog;
    }
}

//const CotationDialogModule = getModule(CotationDialog);
//console.dir(CotationDialogModule);
//export default class CotationDialogModule;