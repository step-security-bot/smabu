/* tslint:disable */
/* eslint-disable */
/**
 * LIT.Smabu.API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

import { CustomerId } from './customer-id';
import { CustomerNumber } from './customer-number';
 /**
 * 
 *
 * @export
 * @interface CreateCustomerCommand
 */
export interface CreateCustomerCommand {

    /**
     * @type {CustomerId}
     * @memberof CreateCustomerCommand
     */
    customerId: CustomerId;

    /**
     * @type {string}
     * @memberof CreateCustomerCommand
     */
    name: string | null;

    /**
     * @type {CustomerNumber}
     * @memberof CreateCustomerCommand
     */
    number?: CustomerNumber;
}
