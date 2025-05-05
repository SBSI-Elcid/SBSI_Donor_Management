<template>
	<div class="container">
		<h1 class="text-center">{{ testOrder.InstitutionName }}</h1>
		<h2 class="text-center">Test Order Report</h2>

		<table class="table table-bordered">
			<tr>
				<td>Test Order Number:  <span> <strong>{{ testOrder.TestOrderNumber }}</strong></span></td>
				<td>Test Completed:  <span><strong>{{ testOrder.TestCompleted }}</strong></span></td>
				<td>Date Created:  <span><strong>{{ testOrder.DateCreated }}</strong></span></td>
			</tr>
			<tr>
				<td>Patient Number:  <span><strong>{{ testOrder.PatientIdNo }}</strong></span></td>
				<td>Patient Name:  <span><strong>{{ testOrder.PatientName }}</strong></span></td>
				<td>Blood Type:  <span><strong>{{ testOrder.BloodType }}</strong></span></td>
			</tr>
		</table>

		<div v-if="testOrder.CrossMatchTestOrders.length > 0">
			<p class="mb-3 mt-3 font-weight-bold">Compatibility Tests</p>

			<table class="table table-bordered table-sm">
				<tr>
					<th scope="col">Donor Unit Serial Number</th>
					<th scope="col">Component</th>
					<th scope="col">Cross Match Type</th>
					<th scope="col">LISS/AGH</th>
					<th scope="col">Result</th>
				</tr>

				<tr v-for="(item, index) in testOrder.CrossMatchTestOrders" :key="index">
					<td><span>{{ item.DonorUnitSerialNumber }}</span></td>
					<td><span>{{ item.BloodComponentName }}</span></td>
					<td><span>{{ item.CrossMatchType }}</span></td>
					<td><span>{{ item.LISS_AGH }}</span></td>
					<td><span>{{ item.Result }}</span></td>
				</tr>
			</table>
		</div>

    <div v-if="testOrder.BloodTypingTestOrder">
			<p class="mb-3 mt-3 font-weight-bold">Blood Typing Test Result</p>

			<table class="table table-bordered table-sm">
				<tr>
					<th colspan="5">Forward Typing</th>
					<th colspan="2">Reverse Typing</th>
          <th colspan="2">Result</th>
				</tr>
        <tr>
					<td>Anti A</td>
					<td>Anti B</td>
          <td>Anti AB</td>
          <td>Anti D</td>
          <td>Anti D'</td>
          <td>A Cells</td>
          <td>B Cells</td>
          <td>Blood Type</td>
          <td>Rh Type</td>
				</tr>
				<tr>
					<td>{{ testOrder.BloodTypingTestOrder.FTAntiA }}</td>
					<td>{{ testOrder.BloodTypingTestOrder.FTAntiB }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.FTAntiAB }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.FTAntiD }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.FTAntiD2 }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.RTACells }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.RTBCells }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.BloodType }}</td>
          <td>{{ testOrder.BloodTypingTestOrder.BloodRh }}</td>
				</tr>
			</table>
		</div>
		
    <div v-if="testOrder.CoombsTestOrder">
			<p class="mb-3 mt-3 font-weight-bold">Coombs Test Result</p>

			<table class="table table-bordered table-sm">
				<tr>
					<th>Direct Anti Human Globulin (DAT)</th>
					<th>Indirect Anti Human Globulin (IAT)</th>
				</tr>
				<tr>
					<td>{{ testOrder.CoombsTestOrder.DATResult }}</td>
					<td>{{ testOrder.CoombsTestOrder.IATResult }}</td>
				</tr>
			</table>
		</div>

    <div v-if="testOrder.BloodScreeningTestOrder">
			<p class="mb-3 mt-3 font-weight-bold">Blood Screening Test Result</p>

			<table class="table table-bordered table-sm">
				<tr>
					<th>Result</th>
				</tr>
				<tr>
					<td>{{ testOrder.BloodScreeningTestOrder.Result }}</td>
				</tr>
			</table>
		</div>

    <div class="container" style="padding-bottom: 20px;" v-if="testOrder.Remarks">
      <p class="font-weight-bold">Remarks:</p>
      <p style="padding-top: 1px;">{{ testOrder.Remarks }}</p>
    </div>

    <div class="container" v-if="testOrder.PerformedBy">
      <div class="row pb-2">
        <div class="col font-weight-bold">Performed By</div>
        <div class="col font-weight-bold">Reviewed By</div>
        <div class="col font-weight-bold">Validated By</div>
        <div class="col font-weight-bold">Phatologist</div>
      </div>

      <div class="row pa-0">
        <div class="col">
          <p class="font-weight-normal"><u>{{ testOrder.PerformedBy.Name }}</u></p>
          <p class="font-weight-normal"><span class="font-italic">License No:</span>  {{ testOrder.PerformedBy.LicenseNo }}</p>
        </div>
        <div class="col">
          <p class="font-weight-normal"><u>{{ testOrder.ReviewedBy.Name }}</u></p>
          <p class="font-weight-normal"><span class="font-italic">License No:</span> {{ testOrder.ReviewedBy.LicenseNo }}</p>
        </div>
        <div class="col">
          <p class="font-weight-normal"><u>{{ testOrder.ValidatedBy.Name }}</u></p>
          <p class="font-weight-normal"><span class="font-italic">License No:</span> {{ testOrder.ValidatedBy.LicenseNo }}</p>
        </div>
        <div class="col"> 
          <p class="font-weight-normal"><u>{{ testOrder.Phatologist.Name }}</u></p>
          <p class="font-weight-normal"><span class="font-italic">License No:</span> {{ testOrder.Phatologist.LicenseNo }}</p>
        </div>
      </div>
		</div>
	</div>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { TestOrderReportDto } from '@/models/Reports/TestOrderReportDto';
import { Component, Prop, Watch } from 'vue-property-decorator';

@Component( { components: {} })
export default class TestOrderReport extends VueBase {
	@Prop({ required: true }) public testOrder!: TestOrderReportDto;
}

</script>