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

import { Address } from './address';
import { Communication } from './communication';
import { CorporateDesign } from './corporate-design';
import { CustomerId } from './customer-id';
 /**
 * 
 *
 * @export
 * @interface UpdateCustomerCommand
 */
export interface UpdateCustomerCommand {

    /**
     * @type {CustomerId}
     * @memberof UpdateCustomerCommand
     */
    customerId: CustomerId;

    /**
     * @type {string}
     * @memberof UpdateCustomerCommand
     */
    name: string | null;

    /**
     * @type {string}
     * @memberof UpdateCustomerCommand
     */
    industryBranch: string | null;

    /**
     * @type {Address}
     * @memberof UpdateCustomerCommand
     */
    mainAddress?: Address;

    /**
     * @type {Communication}
     * @memberof UpdateCustomerCommand
     */
    communication?: Communication;

    /**
     * @type {CorporateDesign}
     * @memberof UpdateCustomerCommand
     */
    corporateDesign?: CorporateDesign;
}
